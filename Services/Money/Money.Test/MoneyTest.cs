using System;
using Xunit;
using MassTransit;
using Commons.MqCommands;
using Commons.Domain.Models;
using Commons.Extenssions.Defines;

namespace Money.Test
{
    public class MoneyTest
    {

        IBusControl _mqBus;
        public MoneyTest()
        {
            _mqBus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost/SkyWatch"), h =>
                {
                    h.Username("SkyWatch");
                    h.Password("sky_watch_2019_best");
                });
                
            });
            _mqBus.Start();
        }



        [Fact]
        public void GetMoney()
        {
            IRequestClient<GetMoneyMqCommand> client = _mqBus.CreateRequestClient<GetMoneyMqCommand>
               (new Uri("rabbitmq://localhost/SkyWatch/Money"));
            var response = client.GetResponse<BodyResponse<MoneyMqResponse>>(new GetMoneyMqCommand(10000000002));

            response.Wait();

            var moneyRes = response.Result;

            Assert.Equal(StatuCodeDefines.Success, moneyRes.Message.StatusCode);
            
        }
    }
}
