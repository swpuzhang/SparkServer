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
    public class GetSelfAccountCommandHandler :
        IRequestHandler<GetSelfAccountCommand, BodyResponse<AccountDetail>>
    {
        protected readonly IMediatorHandler _bus;
        private readonly IAccountInfoRepository _accountRepository;
        private readonly  IUserIdGenRepository _genRepository;
        private readonly IAccountRedisRepository _redis;
        private readonly IBusControl _mqBus;
        private readonly IRequestClient<GetMoneyMqCommand> _moneyClient;

        private readonly IMapper _mapper;
        public GetSelfAccountCommandHandler(IAccountInfoRepository rep,
            IUserIdGenRepository genRepository,
            IAccountRedisRepository redis,
            IMediatorHandler bus,
            IBusControl mqBus, IMapper mapper, 
            IRequestClient<GetMoneyMqCommand> moneyClient)
        {
            _accountRepository = rep;
            _genRepository = genRepository;
            _redis = redis;
            _bus = bus;
            _mqBus = mqBus;
            _mapper = mapper;
            _moneyClient = moneyClient;
        }


        public async Task<BodyResponse<AccountDetail>> Handle(GetSelfAccountCommand request, CancellationToken cancellationToken)
        {
            //读取redis account信息
            var tAccount = _redis.GetAccountInfo(request.Id);
            var tMoney = _moneyClient.GetResponseExt<GetMoneyMqCommand, BodyResponse<MoneyMqResponse>>
                            (new GetMoneyMqCommand(request.Id));
       
            var tLevel = _bus.SendCommand(new GetLevelInfoCommand(request.Id));
            var tGame = _bus.SendCommand(new GetGameInfoCommand(request.Id));
            await Task.WhenAll(tAccount, tMoney, tLevel, tGame);

            var accountInfo =  tAccount.Result;
            var moneyInfores = tMoney.Result;
            var moneyInfo = new MoneyInfo(moneyInfores.Message.Body.CurCoins + moneyInfores.Message.Body.Carry, moneyInfores.Message.Body.CurDiamonds,
                moneyInfores.Message.Body.MaxCoins, moneyInfores.Message.Body.MaxDiamonds);
            var levelInfo = tLevel.Result.Body;
            var gameInfo = tGame.Result.Body;
        
            if (accountInfo == null || moneyInfo == null || levelInfo == null || gameInfo == null)
            {
                return new BodyResponse<AccountDetail>(StatusCodeDefines.AccountError,
                    null, null);
            }
            BodyResponse<AccountDetail> response = new BodyResponse<AccountDetail>(StatusCodeDefines.Success,
                null, new AccountDetail(accountInfo.Id, accountInfo.PlatformAccount,
                accountInfo.UserName, accountInfo.Sex, accountInfo.HeadUrl,
                accountInfo.Type, levelInfo, gameInfo, moneyInfo));
            
            return response;

        }
    }
}
