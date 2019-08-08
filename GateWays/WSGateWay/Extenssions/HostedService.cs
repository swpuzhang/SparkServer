using Commons.MqEvents;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WSGateWay.Manager;

namespace WSGateWay.Extenssions
{
    public class HostedService : IHostedService
    {
        private readonly IBusControl _busControl;
        private Timer _timer;
        private readonly IConfiguration _configration;
        private readonly UserConnManager _connManager;

        public HostedService(IBusControl busControl, IConfiguration configration, UserConnManager connManager)
        {
            _busControl = busControl;
            _configration = configration;
            _connManager = connManager;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return _busControl.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Dispose();
            return _busControl.StopAsync(cancellationToken);
        }

        private void DoWork(object state)
        {
            _busControl.Publish<HostInfoEvent>(new HostInfoEvent(_configration["WSHost"], _connManager.GetUserCount()));
        }
    }
}
