using Dummy.Domain.Commands;
using Dummy.Domain.Models;
using Dummy.Domain.RepositoryInterface;
using Commons.Domain.Bus;
using Commons.Domain.CommandHandler;
using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using Commons.Extenssions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Commons.MqCommands;
using Dummy.Domain.Manager;
using Dummy.MqCommands;
using Serilog;

namespace Dummy.Domain.CommandHandlers
{
    public class DummyMatchingCommandHandler :
        IRequestHandler<DummyPlaynowCommand, BodyResponse<DummyMatchingResponseInfo>>, 
        IRequestHandler<BlindMatchingCommand, BodyResponse<DummyMatchingResponseInfo>>
        
    {
 
        protected readonly IMediatorHandler _bus;
        private readonly IDummyRedisRepository _redis;
        private readonly IRequestClient<GetMoneyMqCommand> _moneyClient;
        private readonly MatchingManager _matchingManager;
        private readonly RoomManager _roomManager;
        public DummyMatchingCommandHandler(
            IDummyRedisRepository redis,
            IMediatorHandler bus,
            IRequestClient<GetMoneyMqCommand> moneyClient,
            MatchingManager matchingManager, RoomManager roomManager)
        {
            _redis = redis;
            _bus = bus;
            _moneyClient = moneyClient;
            _matchingManager = matchingManager;
            _roomManager = roomManager;
        }
        public async Task<BodyResponse<DummyMatchingResponseInfo>> Handle(DummyPlaynowCommand request, CancellationToken cancellationToken)
        {
            //获取玩家金币
            //根据金币判断玩家的场次
            var moneyResponse = await _moneyClient.GetResponseExt<GetMoneyMqCommand, BodyResponse<MoneyMqResponse>>(new GetMoneyMqCommand(request.Id));
            if (moneyResponse.Message.StatusCode != StatusCodeDefines.Success)
            {
                return new BodyResponse<DummyMatchingResponseInfo>(moneyResponse.Message.StatusCode, null);
            }
            long curCoins = moneyResponse.Message.Body.CurCoins;
            if  (!_matchingManager.GetBlindFromCoins(curCoins, out var blind))
            {
                return new BodyResponse<DummyMatchingResponseInfo>(StatusCodeDefines.NoEnoughMoney, null, null);
            }
            var response = await _matchingManager.MatchingRoom(request.Id, blind, "");
            //BodyResponse<DummyMatchingResponseInfo> response = new BodyResponse<DummyMatchingResponseInfo>(StatusCodeDefines.LoginError, null, null);
            return response;

        }

        public async Task<BodyResponse<DummyMatchingResponseInfo>> Handle(BlindMatchingCommand request, CancellationToken cancellationToken)
        {
            var moneyResponse = await _moneyClient.GetResponseExt<GetMoneyMqCommand, BodyResponse<MoneyMqResponse>>(new GetMoneyMqCommand(request.Id));
            if (moneyResponse.Message.StatusCode != StatusCodeDefines.Success)
            {
                return new BodyResponse<DummyMatchingResponseInfo>(moneyResponse.Message.StatusCode, null);
            }
            long curCoins = moneyResponse.Message.Body.CurCoins;
            if (!_roomManager.CoinsIsAvailable(curCoins, request.Blind))
            {
                return new BodyResponse<DummyMatchingResponseInfo>(StatusCodeDefines.NoEnoughMoney, null);
            }
            var response = await _matchingManager.MatchingRoom(request.Id, request.Blind, "");
            return response;
        }
    }
}
