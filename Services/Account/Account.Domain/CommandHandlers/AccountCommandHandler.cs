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
using Account.Domain.Manager;

namespace Account.Domain.CommandHandlers
{
    public class AccountCommandHandler :
        IRequestHandler<LoginCommand, BodyResponse<AccountResponse>>
    {
        protected readonly IMediatorHandler _bus;
        private readonly IAccountInfoRepository _accountRepository;
        private readonly  IUserIdGenRepository _genRepository;
        private readonly IAccountRedisRepository _redis;
        private readonly IRequestClient<GetMoneyMqCommand> _moneyClient;
        private readonly IRequestClient<AddMoneyMqCommand> _moneyAddClient;
        private readonly IMapper _mapper;
        private readonly WSHostManager _hostManager;
        private readonly InitRewardInfo _initRewardInfo;
        public AccountCommandHandler(IAccountInfoRepository rep,
            IUserIdGenRepository genRepository,
            IAccountRedisRepository redis,
            IMediatorHandler bus,
            IMapper mapper,
            IRequestClient<GetMoneyMqCommand> moneyClient,
            WSHostManager hostManager, InitRewardInfo initRewardInfo, 
            IRequestClient<AddMoneyMqCommand> moneyAddClient)
        {
            _accountRepository = rep;
            _genRepository = genRepository;
            _redis = redis;
            _bus = bus;
            _mapper = mapper;
            _moneyClient = moneyClient;
            _hostManager = hostManager;
            _initRewardInfo = initRewardInfo;
            _moneyAddClient = moneyAddClient;
        }


        public async Task<BodyResponse<AccountResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var newAccountInfo = request.Info;
            //根据PlatformAccount字段读取redis，判断该账号是否已经注册
            var loginCheckInfo = await _redis.GetLoginCheckInfo(request.Info.PlatformAccount);
            AccountInfo accountInfo = null;
            bool isRegister = false;
            if (loginCheckInfo != null)
            {
                //直接通过ID去查找这个玩家信息
                accountInfo = await _redis.GetAccountInfo(loginCheckInfo.Id);
                if (accountInfo == null)
                {
                    accountInfo = await _accountRepository.GetByIdAsync(loginCheckInfo.Id);
                }
                
            }

            else
            {
                //查找数据库中是否有这个账号
                accountInfo = await _accountRepository.GetByPlatform(newAccountInfo.PlatformAccount);
                if (accountInfo == null)
                {

                    //注册新账号
                    isRegister = true;
                    long newUid = await _genRepository.GenNewId();
                    accountInfo = new AccountInfo(newUid, newAccountInfo.PlatformAccount, 
                        newAccountInfo.UserName, newAccountInfo.Sex, newAccountInfo.HeadUrl, 
                        newAccountInfo.Type, DateTime.Now, GetAccountBaseInfoMqResponse.SomeFlags.None);
                    await _accountRepository.AddAsync(accountInfo); 
                }
            }

            if (accountInfo != null)
            {
                newAccountInfo.Id = accountInfo.Id;
                string token = TokenHelper.GenToken(accountInfo.Id);
                AccountResponse accounResponse = null; 
                
                bool isNeedUpdate = false;
                //如果登录信息有更新, 那么更新数据库
                if (!isRegister && accountInfo != newAccountInfo)
                {
                    isNeedUpdate = true;
                }
                if (isRegister)
                {
                    var mqResponse = await _moneyAddClient.GetResponseExt<AddMoneyMqCommand, BodyResponse<MoneyMqResponse>>
                           (new AddMoneyMqCommand(accountInfo.Id, _initRewardInfo.RewardCoins, 0, AddReason.InitReward));
                    var moneyInfo = mqResponse.Message.Body;

                    accounResponse = new AccountResponse(newAccountInfo.Id,
                    newAccountInfo.PlatformAccount,
                    newAccountInfo.UserName,
                    newAccountInfo.Sex,
                    newAccountInfo.HeadUrl,
                    token, new MoneyInfo(moneyInfo.CurCoins + moneyInfo.Carry,
                    moneyInfo.CurDiamonds,
                    moneyInfo.MaxCoins,
                    moneyInfo.MaxDiamonds),
                    _hostManager.GetOneHost(),true, newAccountInfo.Type);
                }
                else
                {
                    //查询玩家金币
                    MoneyMqResponse moneyResponse = null;
                   
                    var mqResponse = await _moneyClient.GetResponseExt<GetMoneyMqCommand, BodyResponse<MoneyMqResponse>>
                            (new GetMoneyMqCommand(accountInfo.Id));
                    moneyResponse = mqResponse.Message.Body;
                    accounResponse = new AccountResponse(newAccountInfo.Id,
                    newAccountInfo.PlatformAccount,
                    newAccountInfo.UserName,
                    newAccountInfo.Sex,
                    newAccountInfo.HeadUrl,
                    token, new MoneyInfo(moneyResponse.CurCoins + moneyResponse.Carry,
                    moneyResponse.CurDiamonds,
                    moneyResponse.MaxCoins,
                    moneyResponse.MaxDiamonds),
                    _hostManager.GetOneHost(), false, newAccountInfo.Type);
                }

                _ = _bus.RaiseEvent<LoginEvent>(new LoginEvent(Guid.NewGuid(), 
                    accounResponse, isRegister, isNeedUpdate, newAccountInfo));
                BodyResponse<AccountResponse> retRresponse =
                    new BodyResponse<AccountResponse>(StatusCodeDefines.Success, null, accounResponse);
                return retRresponse;
            }

            BodyResponse<AccountResponse> response = new BodyResponse<AccountResponse>(StatusCodeDefines.LoginError,
                null, null);
            
            return response;

        }
    }
}
