using Commons.Domain.Models;
using Commons.MqCommands;
using Friend.Application.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friend.WebApi.MqConsumers
{
    public class FriendConsumer :
        IConsumer<GetFriendInfoMqCommand>
    {

        IFriendService _service;
        public FriendConsumer(IFriendService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<GetFriendInfoMqCommand> context)
        {
            var response = await _service.GetFriendInfo(context.Message.Id, context.Message.OtherId);
            await context.RespondAsync(response);
        }
    }
}
