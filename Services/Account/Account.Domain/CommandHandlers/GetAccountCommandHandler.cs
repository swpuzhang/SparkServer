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
using Account.Domain.Manager;

namespace Account.Domain.CommandHandlers
{
    public class GetAccountCommandHandler :
        IRequestHandler<GetSelfAccountCommand, BodyResponse<AccountDetail>>,
        IRequestHandler<GetAccountBaseInfoCommand, BodyResponse<AccountInfo>>,
        IRequestHandler<GetIdByPlatformCommand, BodyResponse<GetIdByPlatformMqResponse>>,
        IRequestHandler<GetOtherAccountCommand, BodyResponse<OtherAccountDetail>>
    {
        protected readonly IMediatorHandler _bus;
        private readonly IAccountInfoRepository _accountRepository;
        private readonly  IUserIdGenRepository _genRepository;
        private readonly IAccountRedisRepository _redis;
        private readonly IBusControl _mqBus;
        private readonly IRequestClient<GetMoneyMqCommand> _moneyClient;
        private readonly IRequestClient<GetFriendInfoMqCommand> _friednClient;
        private readonly IMapper _mapper;
        public GetAccountCommandHandler(IAccountInfoRepository rep,
            IUserIdGenRepository genRepository,
            IAccountRedisRepository redis,
            IMediatorHandler bus,
            IBusControl mqBus, IMapper mapper,
            IRequestClient<GetMoneyMqCommand> moneyClient, 
            IRequestClient<GetFriendInfoMqCommand> friednClient)
        {
            _accountRepository = rep;
            _genRepository = genRepository;
            _redis = redis;
            _bus = bus;
            _mqBus = mqBus;
            _mapper = mapper;
            _moneyClient = moneyClient;
            _friednClient = friednClient;
        }


        public async Task<BodyResponse<AccountDetail>> Handle(GetSelfAccountCommand request, CancellationToken cancellationToken)
        {
            //读取redis account信息
            var accountInfo = await GetAccountDetail(request.Id);
            if (accountInfo == null)
            {
                return new BodyResponse<AccountDetail>(StatusCodeDefines.AccountError);
            }
           
            return  new BodyResponse<AccountDetail>(StatusCodeDefines.Success,
                null, accountInfo);
            

        }

        private async Task<AccountDetail> GetAccountDetail(long id )

        {
            var tAccount = _redis.GetAccountInfo(id);
            var tMoney = _moneyClient.GetResponseExt<GetMoneyMqCommand, BodyResponse<MoneyMqResponse>>
                            (new GetMoneyMqCommand(id));

            var tLevel = _bus.SendCommand(new GetLevelInfoCommand(id));
            var tGame = _bus.SendCommand(new GetGameInfoCommand(id));
            await Task.WhenAll(tAccount, tMoney, tLevel, tGame);

            var accountInfo = tAccount.Result;
            var moneyInfores = tMoney.Result;
            var moneyInfo = new MoneyInfo(moneyInfores.Message.Body.CurCoins + moneyInfores.Message.Body.Carry, moneyInfores.Message.Body.CurDiamonds,
                moneyInfores.Message.Body.MaxCoins, moneyInfores.Message.Body.MaxDiamonds);
            var levelInfo = tLevel.Result.Body;
            var gameInfo = tGame.Result.Body;

            if (accountInfo == null || moneyInfo == null || levelInfo == null || gameInfo == null)
            {
                return null;
            }
            return new AccountDetail(accountInfo.Id, accountInfo.PlatformAccount,
                accountInfo.UserName, accountInfo.Sex, accountInfo.HeadUrl,
                accountInfo.Type, levelInfo, gameInfo, moneyInfo);
        }

        public async Task<BodyResponse<AccountInfo>> Handle(GetAccountBaseInfoCommand request, CancellationToken cancellationToken)
        {
            var accountInfo = await AccountMethods.GetAccountInfo(request.Id, _accountRepository, _redis);
            if (accountInfo == null)
            {
                return new BodyResponse<AccountInfo>(StatusCodeDefines.AccountError, null);
            }
            return new BodyResponse<AccountInfo>(StatusCodeDefines.Success, null, accountInfo);
        }

        public async Task<BodyResponse<GetIdByPlatformMqResponse>> Handle(GetIdByPlatformCommand request, CancellationToken cancellationToken)
        {
            long? id = await AccountMethods.GetIdByPlatForm(request.PlatformAccount, _accountRepository, _redis);
            if  (id != null)
            {
                return new BodyResponse<GetIdByPlatformMqResponse>(StatusCodeDefines.Success, null, new GetIdByPlatformMqResponse(id.Value));
            }

            return new BodyResponse<GetIdByPlatformMqResponse>(StatusCodeDefines.Error);
        }

        public async Task<BodyResponse<OtherAccountDetail>> Handle(GetOtherAccountCommand request, CancellationToken cancellationToken)
        {
            var accountInfo = await GetAccountDetail(request.OtherId);
            if (accountInfo == null)
            {
                return new BodyResponse<OtherAccountDetail>(StatusCodeDefines.AccountError, null);
            }
            var otherinfo = _mapper.Map<OtherAccountDetail>(accountInfo);
            var response = await _friednClient.GetResponseExt<GetFriendInfoMqCommand, BodyResponse<GetFriendInfoMqResponse>>(
                new GetFriendInfoMqCommand(request.Id, request.OtherId));
            if (response.Message.Body != null)
            {
                otherinfo.FriendType = response.Message.Body.FriendType;
            }
            return new BodyResponse<OtherAccountDetail>(StatusCodeDefines.Success, null, otherinfo);
        }
    }
}
