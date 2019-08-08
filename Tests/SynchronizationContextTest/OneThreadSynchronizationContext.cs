using System;
using System.Collections.Concurrent;
using System.Threading;

namespace OneThread
{
	public class OneThreadSynchronizationContext : SynchronizationContext
	{
		public static OneThreadSynchronizationContext Instance { get; } = new OneThreadSynchronizationContext();

		private readonly int mainThreadId = Thread.CurrentThread.ManagedThreadId;

		private readonly ConcurrentQueue<Action> queue = new ConcurrentQueue<Action>();

        BlockingCollection<Action> _queue = new BlockingCollection<Action>();

        //private Action a;

		public void Update()
		{
			while (true)
			{
                
                foreach(var one in _queue.GetConsumingEnumerable())
                {
                    one();
                }
                return;
				/*if (!this.queue.TryDequeue(out a))
				{
					return;
				}
				a();*/
			}
		}

		public override void Post(SendOrPostCallback callback, object state)
		{

			if (Thread.CurrentThread.ManagedThreadId == this.mainThreadId)
			{
				callback(state);
				return;
			}
            _queue.Add(() => { callback(state); });
            //_queue.CompleteAdding();

            //this.queue.Enqueue(() => { callback(state); });
		}

        public static void Run()
        {
            SynchronizationContext.SetSynchronizationContext(OneThreadSynchronizationContext.Instance);
            while (true)
            {
                try
                {
                    Thread.Sleep(1);
                    OneThreadSynchronizationContext.Instance.Update();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
	}
}
