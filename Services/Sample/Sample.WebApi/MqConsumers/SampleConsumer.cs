using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.WebApi.MqConsumers
{
    /*public class SampleConsumer :
        IConsumer<GetMoneyMqCommand>
    {

        IMoneyService _service;
        public SampleConsumer(IMoneyService service)
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
