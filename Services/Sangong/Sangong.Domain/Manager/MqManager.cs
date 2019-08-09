using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Commons.MqCommands;
using Commons.Extenssions;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Commons.Domain.Models;
using Sangong.MqCommands;
using Sangong.MqEvents;

namespace Commons.Domain.Managers
{
    public class MqManager
    {
        public IBusControl _bus { get; private set; }
        //public static string accountUrl;
        //public static string moneyUrl;
        public readonly IRequestClient<GetAccountInfoMqCommand> _accountClient;
        public readonly IRequestClient<GetMoneyMqCommand> _moneyClient;
        public readonly IRequestClient<BuyInMqCommand> _buyInClient;
        private readonly IRequestClient<UserApplySitMqCommand> _sitClient;
        public MqManager(IBusControl bus, 
            IRequestClient<GetAccountInfoMqCommand> accountClient, 
            IRequestClient<GetMoneyMqCommand> moneyClient,
            IRequestClient<UserApplySitMqCommand> sitClient)
        {
            _bus = bus;
            _accountClient = accountClient;
            _moneyClient = moneyClient;
            _sitClient = sitClient;
            //accountUrl = configuration.GetSection("Rabbitmq")["Account"];
            //moneyUrl = configuration.GetSection("Rabbitmq")["Money"];
        }

        public async Task<BaseResponse> UserApplySit(long id, string roomId, string gameKey, long blind)
        {
            try
            {
                var response = await _sitClient.GetResponseExt<UserApplySitMqCommand, BaseResponse>(
                    new UserApplySitMqCommand(id, roomId, gameKey, blind));
                return response.Message;
            }
            catch (Exception)
            {
                return null;
            }

        }

    

        public async Task<GetAccountInfoMqResponse> GetAccountInfo(long id)
        {
            try
            {
                var response = await _accountClient.GetResponseExt<GetAccountInfoMqCommand, GetAccountInfoMqResponse>(new GetAccountInfoMqCommand(id));
                return response.Message;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public async Task<MoneyMqResponse> GetMoneyInfo(long id)
        {
            try
            {
                var response = await _moneyClient.GetResponseExt<GetMoneyMqCommand, BodyResponse<MoneyMqResponse>>(new GetMoneyMqCommand(id));
                return response.Message.Body;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<MoneyMqResponse> BuyIn(long id, long min, long max)
        {
            try
            {
                var response = await _buyInClient.GetResponseExt<BuyInMqCommand, BodyResponse<MoneyMqResponse>>(
                    new BuyInMqCommand(id, min, max));
                return response.Message.Body;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
