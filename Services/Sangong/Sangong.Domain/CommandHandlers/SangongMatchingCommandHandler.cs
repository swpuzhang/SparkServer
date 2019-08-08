using Sangong.Domain.Commands;
using Sangong.Domain.Models;
using Sangong.Domain.RepositoryInterface;
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
using Sangong.Domain.Manager;
using Sangong.MqCommands;

namespace Sangong.Domain.CommandHandlers
{
    public class SangongMatchingCommandHandler :
        IRequestHandler<SangongPlaynowCommand, BodyResponse<SangongMatchingResponseInfo>>
    {
 
        protected readonly IMediatorHandler _bus;
        private readonly ISangongRedisRepository _redis;
        private readonly IRequestClient<GetMoneyMqCommand> _moneyClient;
        private readonly MatchingManager _matchingManager;
        public SangongMatchingCommandHandler(ISangongInfoRepository rep,
            ISangongRedisRepository redis,
            IMediatorHandler bus,
            IRequestClient<GetMoneyMqCommand> moneyClient,
            MatchingManager matchingManager)
        {
            _redis = redis;
            _bus = bus;
            _moneyClient = moneyClient;
            _matchingManager = matchingManager;
        }
        public async Task<BodyResponse<SangongMatchingResponseInfo>> Handle(SangongPlaynowCommand request, CancellationToken cancellationToken)
        {
            //获取玩家金币
            //根据金币判断玩家的场次
            var moneyResponse = await _moneyClient.GetResponseExt<GetMoneyMqCommand, MoneyMqResponse>(new GetMoneyMqCommand(request.Id));
            long curCoins = moneyResponse.Message.CurCoins;
            var blind = _matchingManager.GetBlindFromCoins(curCoins);
            var response = await _matchingManager.MatchingRoom(request.Id, blind, "");
            //BodyResponse<SangongMatchingResponseInfo> response = new BodyResponse<SangongMatchingResponseInfo>(StatuCodeDefines.LoginError, null, null);
            return (response);

        }
    }
}
