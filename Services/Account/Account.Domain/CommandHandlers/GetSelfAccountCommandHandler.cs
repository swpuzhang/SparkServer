﻿using Account.Domain.Commands;
using Account.Domain.Models;
using Account.Domain.RepositoryInterface;
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
using Commons.MqEvents;
using AutoMapper;
using Serilog;
using Account.Domain.Events;
using Commons.MqCommands;

namespace Account.Domain.CommandHandlers
{
    public class GetSelfAccountCommandHandler :
        IRequestHandler<GetSelfAccountCommand, HasBodyResponse<AccountDetail>>
    {
        protected readonly IMediatorHandler _bus;
        private readonly IAccountInfoRepository _accountRepository;
        private readonly  IUserIdGenRepository _genRepository;
        private readonly IAccountRedisRepository _redis;
        private readonly IBusControl _mqBus;
        private readonly IRequestClient<GetMoneyMqResponse> _moneyClient;
        private readonly IMapper _mapper;
        public GetSelfAccountCommandHandler(IAccountInfoRepository rep,
            IUserIdGenRepository genRepository,
            IAccountRedisRepository redis,
            IMediatorHandler bus,
            IBusControl mqBus, IMapper mapper, 
            IRequestClient<GetMoneyMqResponse> moneyClient)
        {
            _accountRepository = rep;
            _genRepository = genRepository;
            _redis = redis;
            _bus = bus;
            _mqBus = mqBus;
            _mapper = mapper;
            _moneyClient = moneyClient;
        }


        public async Task<HasBodyResponse<AccountDetail>> Handle(GetSelfAccountCommand request, CancellationToken cancellationToken)
        {
            //读取redis account信息
            var tAccount = _redis.GetAccountInfo(request.Id);
            var tMoney = _moneyClient.GetResponse<GetMoneyMqResponse>
                            (new GetMoneyMqCommand(request.Id));
          
            var tLevel = _bus.SendCommand(new GetLevelInfoCommand(request.Id));
            var tGame = _bus.SendCommand(new GetGameInfoCommand(request.Id));
            var accountInfo = await tAccount;
            var moneyInfo = await tMoney;
            var levelInfo = await tLevel;
            var gameInfo = await tGame;
            if (accountInfo == null || moneyInfo == null || levelInfo == null || gameInfo == null)
            {
                return new HasBodyResponse<AccountDetail>(StatuCodeDefines.AccountError,
                    null, null);
            }
            HasBodyResponse<AccountDetail> response = new HasBodyResponse<AccountDetail>(StatuCodeDefines.Success,
                null, new AccountDetail(accountInfo.Id, accountInfo.PlatformAccount,
                accountInfo.UserName, accountInfo.Sex, accountInfo.HeadUrl,
                accountInfo.Type, levelInfo.Body, gameInfo.Body));
            
            return response;

        }
    }
}
