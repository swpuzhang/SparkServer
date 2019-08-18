using Commons.Domain.Bus;
using Commons.Domain.Models;
using Commons.Extenssions;
using Commons.Extenssions.Defines;
using MassTransit;
using MediatR;
using Reward.Domain.Commands;
using Reward.Domain.Models;
using Reward.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Commons.MqCommands;

namespace Reward.Domain.CommandHandlers
{
    public class BankruptcyCommandHandler :
        IRequestHandler<QueryBankruptcyCommand, BodyResponse<BankruptcyInfoVM>>,
        IRequestHandler<GetBankruptcyRewardCommand, BodyResponse<RewardInfoVM>>

    {
        protected readonly IMediatorHandler _bus;
        private readonly IRewardRedisRepository _redis;
        private readonly BankruptcyConfig _bankruptcyConfig;
        private readonly IBusControl _mqBus;

        public BankruptcyCommandHandler(IMediatorHandler bus, IRewardRedisRepository redis,
            BankruptcyConfig bankruptcyConfig, IBusControl mqBus)
        {
            _bus = bus;
            _redis = redis;
            _bankruptcyConfig = bankruptcyConfig;
            _mqBus = mqBus;
        }

        public async Task<BodyResponse<BankruptcyInfoVM>> Handle(QueryBankruptcyCommand request, CancellationToken cancellationToken)
        {
            //查询当天redis记录
            DateTime tnow = DateTime.Now;
            var bankruptcyInfo = await _redis.GetBankruptcyInfo(tnow, request.Id);
            int totalTimes = _bankruptcyConfig.BankruptcyRewards.Count;
            int curTimes = 0;
            if (bankruptcyInfo == null)
            {
                curTimes = 0;
               
            }
            else
            {
                curTimes = bankruptcyInfo.CurTimes;
            }
            return new BodyResponse<BankruptcyInfoVM>(StatusCodeDefines.Success, null,
                new BankruptcyInfoVM(BankruptcyInfoVM.BankruptcyRewardType.Day, totalTimes, curTimes,
                    _bankruptcyConfig.BankruptcyRewards));
        }

        public async Task<BodyResponse<RewardInfoVM>> Handle(GetBankruptcyRewardCommand request, CancellationToken cancellationToken)
        {
            DateTime tnow = DateTime.Now;
            using (var locker = _redis.Loker(KeyGenHelper.GenUserDayKey(tnow, request.Id, BankruptcyInfo.className)))
            {
                await locker.LockAsync();
                var bankruptcyInfo = await _redis.GetBankruptcyInfo(tnow, request.Id);
                int totalTimes = _bankruptcyConfig.BankruptcyRewards.Count;
                if (bankruptcyInfo == null)
                {
                    bankruptcyInfo = new BankruptcyInfo(0);
                }
                if (bankruptcyInfo.CurTimes >= 2)
                {
                    return new BodyResponse<RewardInfoVM>(StatusCodeDefines.RewardNotAvailable);
                }
                long rewardCoins = _bankruptcyConfig.BankruptcyRewards[bankruptcyInfo.CurTimes];
                ++bankruptcyInfo.CurTimes;
                await _redis.SetBankruptcyInfo(tnow, bankruptcyInfo);
                _ = _mqBus.Publish(new AddMoneyMqCommand(request.Id, rewardCoins, 0, MoneyReson.Bankruptcy));
                return new BodyResponse<RewardInfoVM>(StatusCodeDefines.Success, null, new RewardInfoVM(rewardCoins));
            }
        }
    }
}
