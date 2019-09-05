using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadFile.WebApi.MqConsumers
{
    /*public class UploadFileConsumer :
        IConsumer<GetMoneyMqCommand>
    {

        IMoneyService _service;
        public UploadFileConsumer(IMoneyService service)
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
