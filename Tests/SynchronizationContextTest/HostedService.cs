
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
namespace SynchronizationContextTest
{
    public class HostedService : IHostedService
    {

        public static System.Timers.Timer _timer;
        private double interval = 1000;
        public HostedService()
        {
           
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"StartAsync cur thread{Thread.CurrentThread.ManagedThreadId}------------++++++");
            _timer = new System.Timers.Timer(interval);
            _timer.Elapsed += DoWork;
            _timer.Enabled = true;
            _timer.AutoReset = false;
           
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Dispose();
            return Task.CompletedTask;
        }

        private void DoWork(object state, ElapsedEventArgs e)
        {
            Console.WriteLine($"DoWork cur thread{Thread.CurrentThread.ManagedThreadId}------------{e.SignalTime}");
            _timer.Stop();
            interval += 1000;
            _timer.Interval = interval;
            _timer.Start();
        }
    }
}
