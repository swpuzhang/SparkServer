using GameSangong.Domain.MqCommands;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSGateWay.Services;

namespace WSGateWay.IntegrationHandlers
{
    public class RoomMatchingConsumer : IConsumer<RoomIdMapConfigCommand>
    {

        ICommandService _service;

        public RoomMatchingConsumer(ICommandService service)
        {
            _service = service;
        }

        public Task Consume(ConsumeContext<RoomIdMapConfigCommand> context)
        {
            _service.OnRoomGameMapConfig(context.Message);
            return Task.CompletedTask;
        }
    }
}
