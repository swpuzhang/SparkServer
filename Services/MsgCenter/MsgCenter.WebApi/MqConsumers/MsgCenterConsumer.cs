using Commons.MqCommands;
using MassTransit;
using MsgCenter.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsgCenter.WebApi.MqConsumers
{
    public class MsgCenterConsumer :
        IConsumer<PushUserMsgMqCommand>
    {
        private readonly IMsgCenterService _service;
        public MsgCenterConsumer(IMsgCenterService service)
        {
            _service = service;
        }

        public  async Task Consume(ConsumeContext<PushUserMsgMqCommand> context)
        {
            var response = await _service.PushMsg(context.Message.Id, context.Message.Msg);

            await context.RespondAsync(response);
        }
    }
}
