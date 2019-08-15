using Commons.Domain.Models;
using Commons.MqCommands;
using MassTransit;
using Money.Application.Services;
using System.Threading.Tasks;

namespace Money.WebApi.MqConsumers
{
    public class MoneyConsumer :
        IConsumer<GetMoneyMqCommand>,
        IConsumer<BuyInMqCommand>,
        IConsumer<AddMoneyMqCommand>
    {

        IMoneyService _service;
        public MoneyConsumer(IMoneyService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<GetMoneyMqCommand> context)
        {
            var response = await _service.GetMoney(context.Message.Id);
            await context.RespondAsync<BodyResponse<MoneyMqResponse>>(response);
        }

        public async Task Consume(ConsumeContext<BuyInMqCommand> context)
        {
            var response = await _service.BuyIn(context.Message.Id, context.Message.MinBuy, context.Message.MaxBuy);
            await context.RespondAsync<BodyResponse<MoneyMqResponse>>(response);
        }

        public async Task Consume(ConsumeContext<AddMoneyMqCommand> context)
        {
            var response = await _service.AddMoney(context.Message.Id, context.Message.AddCoins, context.Message.AddCarry);
            await context.RespondAsync<BodyResponse<MoneyMqResponse>>(response);
        }
    }
}
