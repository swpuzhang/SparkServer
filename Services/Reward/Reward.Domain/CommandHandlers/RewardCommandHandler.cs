using Reward.Domain.Commands;
using Reward.Domain.Models;
using Reward.Domain.RepositoryInterface;
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
using Commons.MqEvents;

namespace Reward.Domain.CommandHandlers
{
    public class RewardCommandHandler :
        IRequestHandler<QueryRegisterRewardCommand, BodyResponse<RegisterRewardVM>>,
        IRequestHandler<GetRegisterRewardCommand, BodyResponse<RewardInfoVM>>
    {
        //private readonly readonly IRequestClient<DoSomething> _requestClient;
    
        protected readonly IMediatorHandler _bus;
        private readonly IRegisterRewardRepository _registerRepository;
        private readonly IRewardRedisRepository _redis;
        private readonly IRequestClient<GetAccountBaseInfoMqCommand> _accountClient;
        private readonly IBusControl _mqBus;
        private readonly RegisterRewardConfig _regsterConfig;
        public RewardCommandHandler(IRegisterRewardRepository rep, IRewardRedisRepository redis,
            IMediatorHandler bus, IRequestClient<GetAccountBaseInfoMqCommand> accountClient,
            RegisterRewardConfig regsterConfig, IBusControl mqBus)
        {
            _registerRepository = rep;
            _redis = redis;
            _bus = bus;
            _accountClient = accountClient;
            _regsterConfig = regsterConfig;
            _mqBus = mqBus;
        }


        public async Task<BodyResponse<RegisterRewardVM>> Handle(QueryRegisterRewardCommand request, CancellationToken cancellationToken)
        {
            //获取列表

            //获取该玩家的注册时间，从注册的第二天起才能领取注册奖励
            var accountResponse = await _accountClient.GetResponseExt<GetAccountBaseInfoMqCommand, BodyResponse<GetAccountBaseInfoMqResponse>>
                (new GetAccountBaseInfoMqCommand(request.Id));
            var accountInfo = accountResponse.Message;
            if (accountInfo.StatusCode != StatusCodeDefines.Success)
            {
                return new BodyResponse<RegisterRewardVM>(StatusCodeDefines.Error);
            }
            if ((accountInfo.Body.Flags & GetAccountBaseInfoMqResponse.SomeFlags.RegisterReward) ==
                GetAccountBaseInfoMqResponse.SomeFlags.RegisterReward)
            {
                return new BodyResponse<RegisterRewardVM>(StatusCodeDefines.Success,
                    null, new RegisterRewardVM(RegisterRewardVM.RewardState.Over, 0, _regsterConfig.DayRewards));
            }
            if (_regsterConfig.DayRewards.Count == 0)
            {
                return new BodyResponse<RegisterRewardVM>(StatusCodeDefines.Success,
                    null, new RegisterRewardVM(RegisterRewardVM.RewardState.None, 0, null));
            }
            DateTime registerDate = accountInfo.Body.RegisterDate.DateOfDayBegin();
            DateTime nowDate = DateTime.Now.DateOfDayBegin();
            if (registerDate == nowDate)
            {
                return new BodyResponse<RegisterRewardVM>(StatusCodeDefines.Success,
                    null, new RegisterRewardVM(RegisterRewardVM.RewardState.NotBegin, 0, _regsterConfig.DayRewards));
            }

            int dayIndex = 0;
            long rewardCoins = 0;
            var rewardInfo = await _redis.GetUserRegiserReward(request.Id);
            if (rewardInfo == null)
            {
                rewardInfo = await _registerRepository.GetByIdAsync(request.Id);

            }
            if (rewardInfo == null)
            {
                rewardCoins = _regsterConfig.DayRewards[dayIndex];
                dayIndex = 0;
            }
            else
            {
                if (rewardInfo.DayIndex >= _regsterConfig.DayRewards.Count - 1)
                {
                    if (dayIndex >= _regsterConfig.DayRewards.Count - 1)
            {
                _ = _mqBus.Publish(new FinishedRegisterRewardMqEvent(request.Id));
            }
                    return new BodyResponse<RegisterRewardVM>(StatusCodeDefines.Success,
                        null, new RegisterRewardVM(RegisterRewardVM.RewardState.Over, 0, _regsterConfig.DayRewards));
                }
                else if (rewardInfo.GetDate.DateOfDayBegin() == nowDate)
                {
                    return new BodyResponse<RegisterRewardVM>(StatusCodeDefines.Success,
                        null, new RegisterRewardVM(RegisterRewardVM.RewardState.Getted, rewardInfo.DayIndex, _regsterConfig.DayRewards));
                }
                else
                {
                    dayIndex = rewardInfo.DayIndex + 1;
                    rewardCoins = _regsterConfig.DayRewards[dayIndex];
                }
            }
            return new BodyResponse<RegisterRewardVM>(StatusCodeDefines.Success, null,
                new RegisterRewardVM(RegisterRewardVM.RewardState.Available, dayIndex, _regsterConfig.DayRewards));


        }

        public async Task<BodyResponse<RewardInfoVM>> Handle(GetRegisterRewardCommand request, CancellationToken cancellationToken)
        {
            var accountResponse = await _accountClient.GetResponseExt<GetAccountBaseInfoMqCommand, BodyResponse<GetAccountBaseInfoMqResponse>>
               (new GetAccountBaseInfoMqCommand(request.Id));
            var accountInfo = accountResponse.Message;
            if (accountInfo.StatusCode != StatusCodeDefines.Success)
            {
                return new BodyResponse<RewardInfoVM>(StatusCodeDefines.Error);
            }
            if ((accountInfo.Body.Flags & GetAccountBaseInfoMqResponse.SomeFlags.RegisterReward) ==
                GetAccountBaseInfoMqResponse.SomeFlags.RegisterReward)
            {
                return new BodyResponse<RewardInfoVM>(StatusCodeDefines.Error);
            }
            if (_regsterConfig.DayRewards.Count == 0)
            {
                return new BodyResponse<RewardInfoVM>(StatusCodeDefines.Error);
            }
            DateTime registerDate = accountInfo.Body.RegisterDate.DateOfDayBegin();
            DateTime nowDate = DateTime.Now.DateOfDayBegin();
            if (registerDate == nowDate)
            {
                return new BodyResponse<RewardInfoVM>(StatusCodeDefines.Error);
            }
            int dayIndex = 0;
            long rewardCoins = 0;
            using (var locker = _redis.Locker(KeyGenHelper.GenUserKey(request.Id, RegisterRewardInfo.className)))
            {
                await locker.LockAsync();
                var rewardInfo = await _redis.GetUserRegiserReward(request.Id);
                if (rewardInfo == null)
                {
                    rewardInfo = await _registerRepository.GetByIdAsync(request.Id);

                }
                if (rewardInfo == null)
                {
                    rewardCoins = _regsterConfig.DayRewards[dayIndex];
                    dayIndex = 0;
                }
                else
                {
                    if (rewardInfo.DayIndex >= _regsterConfig.DayRewards.Count - 1)
                    {
                        return new BodyResponse<RewardInfoVM>(StatusCodeDefines.Error);
                    }
                    else if (rewardInfo.GetDate.DateOfDayBegin() == nowDate)
                    {
                        return new BodyResponse<RewardInfoVM>(StatusCodeDefines.Error);
                    }
                    else
                    {
                        dayIndex = rewardInfo.DayIndex + 1;
                        rewardCoins = _regsterConfig.DayRewards[dayIndex];
                    }
                }

                rewardInfo = new RegisterRewardInfo(request.Id, dayIndex, DateTime.Now);
                await Task.WhenAll(_redis.SetUserRegiserReward(rewardInfo), _registerRepository.ReplaceAndAddAsync(rewardInfo));
            }
            _ = _mqBus.Publish(new AddMoneyMqCommand(request.Id, rewardCoins, 0, AddReason.RegisterReward));
            if (dayIndex >= _regsterConfig.DayRewards.Count - 1)
            {
                _ = _mqBus.Publish(new FinishedRegisterRewardMqEvent(request.Id));
            }
            return new BodyResponse<RewardInfoVM>(StatusCodeDefines.Success, null,
                    new RewardInfoVM(rewardCoins));
        }
    }
}
