using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sangong.Game.WebApi.MqConsumers
{
    /*public class SangongConsumer :
        IConsumer<GetMoneyMqCommand>
    {

        IMoneyService _service;
        public SangongConsumer(IMoneyService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<GetMoneyMqCommand> context)
        {
            var response = await _service.GetMoney(context.Message.Id);
            await context.RespondAsync<BodyResponse<GetMoneyMqResponse>>(response);
        }
    }*/
}
