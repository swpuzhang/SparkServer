﻿using Sangong.Domain.Commands;
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
using Serilog;

namespace Sangong.Domain.CommandHandlers
{
    public class SangongMatchingCommandHandler :
        IRequestHandler<SangongPlaynowCommand, BodyResponse<SangongMatchingResponseInfo>>, 
        IRequestHandler<BlindMatchingCommand, BodyResponse<SangongMatchingResponseInfo>>
        
    {
 
        protected readonly IMediatorHandler _bus;
        private readonly ISangongRedisRepository _redis;
        private readonly IRequestClient<GetMoneyMqCommand> _moneyClient;
        private readonly MatchingManager _matchingManager;
        public SangongMatchingCommandHandler(
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
            var moneyResponse = await _moneyClient.GetResponseExt<GetMoneyMqCommand, BodyResponse<MoneyMqResponse>>(new GetMoneyMqCommand(request.Id));
            if (moneyResponse.Message.StatusCode != StatusCodeDefines.Success)
            {
                return new BodyResponse<SangongMatchingResponseInfo>(moneyResponse.Message.StatusCode, null);
            }
            long curCoins = moneyResponse.Message.Body.CurCoins;
            if  (!_matchingManager.GetBlindFromCoins(curCoins, out var blind))
            {
                return new BodyResponse<SangongMatchingResponseInfo>(StatusCodeDefines.NoEnoughMoney, null, null);
            }
            var response = await _matchingManager.MatchingRoom(request.Id, blind, "");
            //BodyResponse<SangongMatchingResponseInfo> response = new BodyResponse<SangongMatchingResponseInfo>(StatusCodeDefines.LoginError, null, null);
            return response;

        }

        public async Task<BodyResponse<SangongMatchingResponseInfo>> Handle(BlindMatchingCommand request, CancellationToken cancellationToken)
        {
            var moneyResponse = await _moneyClient.GetResponseExt<GetMoneyMqCommand, BodyResponse<MoneyMqResponse>>(new GetMoneyMqCommand(request.Id));
            if (moneyResponse.Message.StatusCode != StatusCodeDefines.Success)
            {
                return new BodyResponse<SangongMatchingResponseInfo>(moneyResponse.Message.StatusCode, null);
            }
            long curCoins = moneyResponse.Message.Body.CurCoins;
            var response = await _matchingManager.MatchingRoom(request.Id, request.Blind, "");
            return response;
        }
    }
}
