using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System.Threading;

namespace TaskTest
{
    public class RpcCaller
    {
       // private readonly ConcurrentDictionary<Guid, KeyValuePair<TaskCompletionSource<string>, Timer>> _pendingMethodCalls = 
        //    new ConcurrentDictionary<Guid, KeyValuePair<TaskCompletionSource<string>, Timer>>();

        private readonly ConcurrentDictionary<Guid, KeyValuePair<TaskCompletionSource<string>, CancellationTokenSource>> _Calls =
            new ConcurrentDictionary<Guid, KeyValuePair<TaskCompletionSource<string>, CancellationTokenSource>>();


        //private readonly IHubContext<THub> _hubContext;
 
        /*public RpcCaller(IHubContext<THub> hubContext)
        {
            //_token.
            _hubContext = hubContext;
        }*/

        public void OnResponsed(Guid id)
        {
            /*if (_pendingMethodCalls.TryRemove(id, out var value))
            {
                value.Value.Dispose();
                value.Key.TrySetResult(new string(StatusCodeDefines.Success, null));
            }*/

            if (_Calls.TryRemove(id, out var value))
            {
                value.Key.TrySetResult("success");
                value.Value.Dispose();
            }

        }

        /*public void OnTimeOut(Object o)
        {
            Guid id = (Guid)o;
            if (_pendingMethodCalls.TryRemove(id, out var value))
            {
                value.Value.Dispose();
                value.Key.TrySetResult(new string(StatusCodeDefines.Timeout, new List<string>() { "timeout" }));
            }
        }*/

        /*public async Task<string> RequestCallAsync(string conn, string method, 
            string reqData, Guid id, int waitMiliSeconds = 5000)
        {
   
            TaskCompletionSource<string> methodCallCompletionSource = new TaskCompletionSource<string>();
            var timer = new Timer(OnTimeOut, id, waitMiliSeconds,Timeout.Infinite);
            
            if (_pendingMethodCalls.TryAdd(id, new KeyValuePair<TaskCompletionSource<string>, Timer>(methodCallCompletionSource, timer)))
            {
                try
                {
                    await _hubContext.Clients.Client(conn).SendAsync(method, reqData);
                }
               
                catch (Exception ex)
                {
                    if (_pendingMethodCalls.TryRemove(id, out var value))
                    {
                        value.Value.Dispose();
                    }
                    return new string(StatusCodeDefines.AppIsDisconnected, new List<string>() { ex.Message });
                }
                
                return await methodCallCompletionSource.Task;
            }
            timer.Dispose();
            return new string(StatusCodeDefines.GuidError, new List<string>() { StatusCodeDefines.GuidError.ToString() });
        }*/

        public async Task<string> RequestCallAsync(string conn, string method,
            string reqData, Guid id, int waitMiliSeconds = 5000)
        {

            TaskCompletionSource<string> methodCallCompletionSource = new TaskCompletionSource<string>();
            CancellationTokenSource token = new CancellationTokenSource(waitMiliSeconds);
            if (_Calls.TryAdd(id, new KeyValuePair<TaskCompletionSource<string>,
                CancellationTokenSource>(methodCallCompletionSource, token)))
            {
                try
                {
                    using (token.Token.Register(
                        () =>
                        {
                            if (_Calls.TryRemove(id, out var value))
                            {
                                value.Key.TrySetResult("timeout");
                                value.Value.Dispose();
                            }
                        }
                        ))
                    {
                        Excute(conn, method, reqData, id);
                        return await methodCallCompletionSource.Task;

                    }  
                }

                catch (Exception ex)
                {
                    if (_Calls.TryRemove(id, out var value))
                    {
                        value.Value.Dispose();
                    }
                    return new string(ex.Message);
                }
                
            }
            else
            {
                token.Dispose();
                return new string("Guid error");
            }
            
        }

        private void Excute(string conn, string method, string data, Guid id)
        {
            Task.Run(() =>
            {
                try
                {
                    Task.Delay(5000).Wait();
                    throw new Exception("excute error");
                   
                }
                catch (Exception ex)
                {
                    if (_Calls.TryRemove(id, out var value))
                    {
                        
                        value.Key.TrySetResult(ex.Message);
                        value.Value.Dispose();
                        
                    }
                }
            });
            
        }
    }
}
