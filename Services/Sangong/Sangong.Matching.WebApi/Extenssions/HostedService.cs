using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sangong.Domain.Manager;
using Sangong.MqEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sangong.Matching.WebApi.Extenssions
{
    public class HostedService : IHostedService
    {
        private readonly IBusControl _busControl;
        private readonly IConfiguration _configuration;
        private readonly MatchingManager _matchingManager;
        private readonly RoomManager _roomManager;
        public HostedService(IBusControl busControl, IConfiguration configuration,
            MatchingManager matchingManager, RoomManager roomManager)
        {
            _busControl = busControl;
            _configuration = configuration;
            _matchingManager = matchingManager;
            _roomManager = roomManager;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await  _busControl.StartAsync(cancellationToken);
            _matchingManager.LoadCoinsRangeConfig();
            _roomManager.LoadRoomListConfig();
            //发送启动命令
            _ = _busControl.Publish<MatchingStartedEvent>(new MatchingStartedEvent(_configuration["MatchingGroup"]));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _busControl.StopAsync(cancellationToken);
        }

        public Task MatchingStarted()
        {
            return _busControl.Publish<MatchingStartedMqEvent>(new MatchingStartedMqEvent(_configuration["MatchingGroup"]));
        }
    }
}
