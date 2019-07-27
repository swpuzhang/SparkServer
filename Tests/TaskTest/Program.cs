using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest
{

    public class TimeoutTask<T>
    {
        #region 字段
        private Func<T> _func;
        private CancellationToken _token;
        private event AsyncCompletedEventHandler _asyncCompletedEvent;
        private TaskCompletionSource<AsyncCompletedEventArgs> _tcs;
        #endregion

        #region 静态方法
        public static async Task<T> StartNewTask(Func<T> func, CancellationToken token,
            int timeout = Timeout.Infinite)
        {
            var task = new TimeoutTask<T>(func, token, timeout);

            return await task.Run();
        }

        public static async Task<T> StartNewTask(Func<T> func, int timeout)
        {
            return await TimeoutTask<T>.StartNewTask(func, CancellationToken.None, timeout);
        }

        public static async Task<T> StartNewTask(Func<T> func, CancellationToken token)
        {
            return await TimeoutTask<T>.StartNewTask(func, token, Timeout.Infinite);
        }
        #endregion

        #region 构造
        protected TimeoutTask(Func<T> func, CancellationToken token) : this(func, token, Timeout.Infinite)
        {

        }

        protected TimeoutTask(Func<T> func, int timeout = Timeout.Infinite) : this(func, CancellationToken.None, timeout)
        {

        }

        protected TimeoutTask(Func<T> func, CancellationToken token, int timeout = Timeout.Infinite)
        {
            _func = func;

            _tcs = new TaskCompletionSource<AsyncCompletedEventArgs>();

            if (timeout != Timeout.Infinite)
            {
                var cts = CancellationTokenSource.CreateLinkedTokenSource(token);
                cts.CancelAfter(timeout);
                _token = cts.Token;
            }
            else
            {
                _token = token;
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 运行Task
        /// </summary>
        /// <returns></returns>
        private async Task<T> Run()
        {
            _asyncCompletedEvent += AsyncCompletedEventHandler;

            try
            {
                using (_token.Register(() => _tcs.SetResult(new AsyncCompletedEventArgs(error: null, cancelled: false, userState: string.Empty))))
                {
                    ExecuteFunc();
                    var args = await _tcs.Task;
                    return (T)args.UserState;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default(T);
            }
            finally
            {
                _asyncCompletedEvent -= AsyncCompletedEventHandler;
            }

        }

        /// <summary>
        /// 执行
        /// </summary>
        private void ExecuteFunc()
        {
            ThreadPool.QueueUserWorkItem(s =>
            {
                var result = _func.Invoke();

                OnAsyncCompleteEvent(result);
            });
        }

        /// <summary>
        /// 异步完成事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AsyncCompletedEventHandler(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                _tcs.TrySetCanceled();
            }
            else if (e.Error != null)
            {
                _tcs.TrySetException(e.Error);
            }
            else
            {
                _tcs.TrySetResult(e);
            }
        }

        /// <summary>
        /// 触发异步完成事件
        /// </summary>
        /// <param name="userState"></param>
        private void OnAsyncCompleteEvent(object userState)
        {
            if (_asyncCompletedEvent != null)
            {
                _asyncCompletedEvent(this, new AsyncCompletedEventArgs(error: null, cancelled: false, userState: userState));
            }
        }
        #endregion
    }

    public static class TaskTest
    {
        public static async Task TaskDealy ()
        {
            await Task.Delay(3000);
            Console.WriteLine("after delay");
        }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            /*var t = TaskTest.TaskDealy();
            Console.WriteLine("Hello World!");
            await t;
            Console.WriteLine("Hello World2");*/

            /*var result = await TimeoutTask<string>.StartNewTask( () => {
                Task.Delay(2000).Wait();
           
                Console.WriteLine("hanle");
                return "success";
            }, 1000);*/
            var rpcCaller = new RpcCaller();
            Guid id = Guid.NewGuid();
            var result = rpcCaller.RequestCallAsync("1", "test", "just a test", id, 7000);
            var result2 = rpcCaller.RequestCallAsync("1", "test", "just a test", id, 7000);
            Task.Run(()=>
            {
                Task.Delay(2000).Wait();
                rpcCaller.OnResponsed(id);
            }).Wait();

            Console.WriteLine(await result);


            //var result2 = rpcCaller.RequestCallAsync("1", "test", "just a test", id, 7000);
            Console.WriteLine(await result2);
            Console.ReadLine();
        }
    }
}
