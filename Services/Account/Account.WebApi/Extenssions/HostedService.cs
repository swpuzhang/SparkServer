using Account.Domain.Manager;
using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Account.WebApi.Extenssions
{
    public class HostedService : IHostedService
    {
        private readonly IBusControl _busControl;
        private readonly WSHostManager _hostManager;
        private Timer _timer;

        public HostedService(IBusControl busControl, WSHostManager hostManager)
        {
            _busControl = busControl;
            _hostManager = hostManager;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(60));
            return _busControl.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Dispose();
            return _busControl.StopAsync(cancellationToken);
        }

        private void DoWork(object state)
        {
            _hostManager.CleanBadHost();
        }
    }
}
