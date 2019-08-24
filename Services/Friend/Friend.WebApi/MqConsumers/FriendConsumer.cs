using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friend.WebApi.MqConsumers
{
    /*public class FriendConsumer :
        IConsumer<GetMoneyMqCommand>
    {

        IMoneyService _service;
        public FriendConsumer(IMoneyService service)
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
