using Commons.Domain.Models;
using Commons.MqCommands;
using MassTransit;
using Money.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Money.WebApi.MqConsumers
{
    public class MoneyConsumer :
        IConsumer<GetMoneyMqCommand>
    {

        IMoneyService _service;
        public MoneyConsumer(IMoneyService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<GetMoneyMqCommand> context)
        {
            var response = await _service.GetMoney(context.Message.Id);
            await context.RespondAsync<BodyResponse<GetMoneyMqResponse>>(response);
        }
    }
}
