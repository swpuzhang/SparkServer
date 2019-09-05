using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using GameSample.MqCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameSample.Game.WebApi.Extenssions
{
    public class HostedService : IHostedService
    {
        private readonly IBusControl _busControl;
        private readonly IConfiguration _configuration;
        
        public HostedService(IBusControl busControl, IConfiguration configuration)
        {
            _busControl = busControl;
            _configuration = configuration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _busControl.StartAsync(cancellationToken);
            var mqcfg = _configuration.GetSection("Rabbitmq");
            var gameKey = mqcfg["Queue"];
            var matchingGroup = _configuration["MatchingGroup"];
            await _busControl.Publish(new SyncGameRoomMqCommand(gameKey, matchingGroup, null));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _busControl.StopAsync(cancellationToken);
        }
    }
}
