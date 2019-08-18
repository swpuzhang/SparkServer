using System;
using Xunit;
using MassTransit;
using Commons.MqCommands;
using Commons.Domain.Models;
using Commons.Extenssions.Defines;
using Commons.Extenssions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Xunit.Abstractions;

namespace Money.Test
{
    public class MoneyTest
    {

        IBusControl _mqBus;
        private readonly ITestOutputHelper _testOutputHelper;
        public MoneyTest(ITestOutputHelper testOutputHelper)
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
            _testOutputHelper = testOutputHelper;
        }



        [Fact]
        public async void GetMoney()
        {
            IRequestClient<GetMoneyMqCommand> client = _mqBus.CreateRequestClient<GetMoneyMqCommand>
               (new Uri("rabbitmq://localhost/SkyWatch/Money"));
            var response = client.GetResponse<BodyResponse<MoneyMqResponse>>(new GetMoneyMqCommand(10000000002));

            var moneyRes = await response;

            _testOutputHelper.WriteLine($"GetMoney:{moneyRes.Message.Body.CurCoins} --01");
            Assert.Equal(StatusCodeDefines.Success, moneyRes.Message.StatusCode);
        }

        [Fact]
        public async void AddMoney()
        {

            IRequestClient<AddMoneyMqCommand> client = _mqBus.CreateRequestClient<AddMoneyMqCommand>
               (new Uri("rabbitmq://localhost/SkyWatch/Money"));
            IRequestClient<GetMoneyMqCommand> getClient = _mqBus.CreateRequestClient<GetMoneyMqCommand>
               (new Uri("rabbitmq://localhost/SkyWatch/Money"));
            var getResponse = getClient.GetResponseExt<GetMoneyMqCommand, BodyResponse<MoneyMqResponse>>(new GetMoneyMqCommand(10000000002));
            getResponse.Wait();
            _testOutputHelper.WriteLine($"GetMoney:{getResponse.Result.Message.Body.CurCoins} --02");
            await client.GetResponseExt<AddMoneyMqCommand, BodyResponse<MoneyMqResponse>>(new AddMoneyMqCommand(10000000002, 10000000, 0, 0));
            Assert.Equal(StatusCodeDefines.Success, getResponse.Result.Message.StatusCode);
           
            /*for (int i = 0; i < 36000000; ++i)
            {
                await client.GetResponseExt<AddMoneyMqCommand, BodyResponse<MoneyMqResponse>>(new AddMoneyMqCommand(10000000002, 10000, 10000));
                await client.GetResponseExt<AddMoneyMqCommand, BodyResponse<MoneyMqResponse>>(new AddMoneyMqCommand(10000000002, -10000, -10000));
                _testOutputHelper.WriteLine($"GetMoney.....");
                Thread.Sleep(1);
                //var response1 = _mqBus.Publish(new AddMoneyMqCommand(10000000002, 10000, 10000));
                //var response2 = _mqBus.Publish(new AddMoneyMqCommand(10000000002, -10000, -10000));
               
                /*Assert.Equal(StatusCodeDefines.Success, moneyRes.Message.StatusCode);
                Assert.Equal(moneyResponse.Body.CurCoins + 10000, response.Result.Message.Body.CurCoins);
                Assert.Equal(moneyResponse.Body.Carry + 10000, response.Result.Message.Body.Carry);

                Assert.Equal(StatusCodeDefines.Success, moneyRes.Message.StatusCode);
                Assert.Equal(moneyResponse.Body.CurCoins, moneyRes.Message.Body.CurCoins);
                Assert.Equal(moneyResponse.Body.Carry, moneyRes.Message.Body.Carry);
            }*/
     
            
            /*var getResponse2 = getClient.GetResponseExt<GetMoneyMqCommand, BodyResponse<MoneyMqResponse>>(new GetMoneyMqCommand(10000000002));
            await getResponse2;
            _testOutputHelper.WriteLine($"GetMoney:{getResponse2.Result.Message.Body.CurCoins} --03");
            Assert.Equal(StatusCodeDefines.Success, getResponse2.Result.Message.StatusCode);
            Assert.Equal(80000, getResponse2.Result.Message.Body.CurCoins);
            Assert.Equal(80000, getResponse2.Result.Message.Body.Carry);
            Assert.Equal(getResponse.Result.Message.Body.CurCoins, getResponse2.Result.Message.Body.CurCoins);
            Assert.Equal(getResponse.Result.Message.Body.Carry, getResponse2.Result.Message.Body.Carry);*/
        }

        [Fact]
        public void BuyIn()
        {
            IRequestClient<BuyInMqCommand> client = _mqBus.CreateRequestClient<BuyInMqCommand>
               (new Uri("rabbitmq://localhost/SkyWatch/Money"));
            IRequestClient<GetMoneyMqCommand> getClient = _mqBus.CreateRequestClient<GetMoneyMqCommand>
               (new Uri("rabbitmq://localhost/SkyWatch/Money"));
            var getResponse = getClient.GetResponseExt<GetMoneyMqCommand, BodyResponse<MoneyMqResponse>>(new GetMoneyMqCommand(10000000002));

            getResponse.Wait();

            Assert.Equal(StatusCodeDefines.Success, getResponse.Result.Message.StatusCode);
            var moneyResponse = getResponse.Result.Message;
            var response = client.GetResponseExt<BuyInMqCommand, BodyResponse<MoneyMqResponse>>(new BuyInMqCommand(10000000002, 1000, 10000, 0));

            response.Wait();

            var moneyRes = response.Result;

            Assert.Equal(StatusCodeDefines.Success, moneyRes.Message.StatusCode);
            
            Assert.True(10000 <= response.Result.Message.Body.Carry);
        }
    }
}
