using Account.Domain.Commands;
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
    public class AccountCommandHandler :
        IRequestHandler<LoginCommand, HasBodyResponse<AccountResponse>>
    {
        protected readonly IMediatorHandler _bus;
        private readonly IAccountInfoRepository _accountRepository;
        private readonly  IUserIdGenRepository _genRepository;
        private readonly IAccountRedisRepository _redis;
        private readonly IBusControl _mqBus;
        private readonly IRequestClient<GetMoneyMqResponse> _moneyClient;
        private readonly IMapper _mapper;
        public AccountCommandHandler(IAccountInfoRepository rep,
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


        public async Task<HasBodyResponse<AccountResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var newAccountInfo = request.Info;
            //根据PlatformAccount字段读取redis，判断该账号是否已经注册
            var loginCheckInfo = await _redis.GetLoginCheckInfo(request.Info.PlatformAccount);
            AccountInfo accountInfo = null;
            bool isRegister = false;
            if (loginCheckInfo != null)
            {
                //直接通过ID去数据库查找这个玩家信息
                accountInfo = await _accountRepository.GetByIdAsync(loginCheckInfo.Id);
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
                        newAccountInfo.UserName, newAccountInfo.Sex, newAccountInfo.HeadUrl, newAccountInfo.Type);
                    await _accountRepository.AddAsync(accountInfo); 
                }
            }

            if (accountInfo != null)
            {
                string token = TokenHelper.GenToken(accountInfo.Id);
                AccountResponse accounResponse = null; 
                HasBodyResponse <AccountResponse> retRresponse = 
                    new HasBodyResponse<AccountResponse>(StatuCodeDefines.Success, null, accounResponse);
                bool isNeedUpdate = false;
                //如果登录信息有更新, 那么更新数据库
                if (!isRegister && accountInfo != newAccountInfo)
                {
                    isNeedUpdate = true;
                }
                if (isRegister)
                {
                    accounResponse = new AccountResponse(accountInfo.Id,
                    accountInfo.PlatformAccount,
                    newAccountInfo.UserName,
                    newAccountInfo.Sex,
                    newAccountInfo.HeadUrl,
                    token,new MoneyInfo());
                }
                else
                {
                    //查询玩家金币
                    GetMoneyMqResponse moneyResponse = null;
                    try
                    {
                        var mqResponse = await _moneyClient.GetResponse<GetMoneyMqResponse>
                            (new GetMoneyMqCommand(accountInfo.Id));
                        moneyResponse = mqResponse.Message;
                    }
                    catch (Exception)
                    {
                        return new HasBodyResponse<AccountResponse>(StatuCodeDefines.GetMoneyError,
                            null, null);
                    }
                    

                    accounResponse = new AccountResponse(accountInfo.Id,
                    accountInfo.PlatformAccount,
                    newAccountInfo.UserName,
                    newAccountInfo.Sex,
                    newAccountInfo.HeadUrl,
                    token, new MoneyInfo(moneyResponse.CurChips,
                    moneyResponse.CurDiamonds,
                    moneyResponse.MaxChips,
                    moneyResponse.MaxDiamonds));
                }

                _ = _bus.RaiseEvent<LoginEvent>(new LoginEvent(Guid.NewGuid(), 
                    accounResponse, isRegister, isNeedUpdate, accountInfo));

         
                
                return retRresponse;
            }

            HasBodyResponse<AccountResponse> response = new HasBodyResponse<AccountResponse>(StatuCodeDefines.LoginError,
                null, null);
            
            return response;

        }
    }
}
