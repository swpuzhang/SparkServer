using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Commons.Domain.Bus;
using Commons.Domain.Models;
using Commons.MqCommands;
using Money.Domain.Commands;
using AutoMapper;
using Commons.Extenssions.Defines;

namespace Money.Application.Services
{
    public class MoneyService : IMoneyService
    {
        private readonly IMediatorHandler _bus;
        private readonly IMapper _mapper;

        public MoneyService(IMediatorHandler bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        public async Task<BodyResponse<MoneyMqResponse>> BuyIn(long id, long min, long max)
        {
            var moneyInfo = await _bus.SendCommand(new BuyInCommand(id, min, max));
            if (moneyInfo.StatusCode != StatusCodeDefines.Success)
            {
                return new BodyResponse<MoneyMqResponse>(moneyInfo.StatusCode, null, null);
            }
            return new BodyResponse<MoneyMqResponse>(StatusCodeDefines.Success, null, moneyInfo.Body);
        }

        public async Task<BodyResponse<MoneyMqResponse>> GetMoney(long id)
        {
            var moneyInfo = await _bus.SendCommand(new GetMoneyCommand(id));
            if (moneyInfo.StatusCode != StatusCodeDefines.Success)
            {
                return new BodyResponse<MoneyMqResponse>(moneyInfo.StatusCode, null, null);
            }
            var moneyResponse = _mapper.Map<MoneyMqResponse>(moneyInfo.Body);
            return new BodyResponse<MoneyMqResponse>(StatusCodeDefines.Success, null, moneyResponse);
        }

        public async Task<BodyResponse<MoneyMqResponse>> AddMoney(long id, long addCoins, long addCarry)
        {
            var moneyInfo = await _bus.SendCommand(new AddMoneyCommand(id, addCoins, addCarry));
            if (moneyInfo.StatusCode != StatusCodeDefines.Success)
            {
                return new BodyResponse<MoneyMqResponse>(moneyInfo.StatusCode, null, null);
            }
            var moneyResponse = _mapper.Map<MoneyMqResponse>(moneyInfo.Body);
            return new BodyResponse<MoneyMqResponse>(StatusCodeDefines.Success, null, moneyResponse);
        }
    }
}
