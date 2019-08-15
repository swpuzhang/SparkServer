using Commons.Domain.Models;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSGateWay.Services;

namespace WSGateWay.IntegrationHandlers
{
    public class ServerRequestConsumer : 
        IConsumer<ServerRequest>,
        IConsumer<GameServerRequest>
    { 
        ICommandService _service;
        public ServerRequestConsumer(ICommandService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<ServerRequest> context)
        {
            await _service.OnServerRequest(context);
           
        }

        public async Task Consume(ConsumeContext<GameServerRequest> context)
        {
            await _service.OnGameRoomRequest(context);
        }
    }

}
