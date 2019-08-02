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

        public async Task<BodyResponse<GetMoneyMqResponse>> GetMoney(long id)
        {
            var moneyInfo = await _bus.SendCommand(new GetMoneyCommand(id));
            if (moneyInfo.StatusCode != StatuCodeDefines.Success)
            {
                return new BodyResponse<GetMoneyMqResponse>(moneyInfo.StatusCode, null, null);
            }
            var moneyResponse = _mapper.Map<GetMoneyMqResponse>(moneyInfo.Body);
            return new BodyResponse<GetMoneyMqResponse>(StatuCodeDefines.Success, null, moneyResponse);
        }
    }
}
