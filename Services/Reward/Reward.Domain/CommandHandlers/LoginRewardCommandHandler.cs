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
    public class LoginRewardCommandHandler :
        IRequestHandler<QueryLoginRewardCommand, BodyResponse<LoginRewardVM>>,
        IRequestHandler<GetLoginRewardCommand, BodyResponse<RewardInfoVM>>

    {
        protected readonly IMediatorHandler _bus;
        private readonly IRewardRedisRepository _redis;
        private readonly LoginRewardConfig _loginConfig;
        private readonly IBusControl _mqBus;

        public LoginRewardCommandHandler(IMediatorHandler bus, IRewardRedisRepository redis, 
            LoginRewardConfig loginConfig, IBusControl mqBus)
        {
            _bus = bus;
            _redis = redis;
            _loginConfig = loginConfig;
            _mqBus = mqBus;
        }

        public async Task<BodyResponse<LoginRewardVM>> Handle(QueryLoginRewardCommand request, CancellationToken cancellationToken)
        {
            //查询本周redis之前的领奖记录
            DateTime tnow = DateTime.Now;
            var rewardInfo = await _redis.GetLoginReward(tnow, request.Id);
            return new BodyResponse<LoginRewardVM>(StatusCodeDefines.Success, null, new LoginRewardVM(GenReward(tnow, rewardInfo)));
        }

        public List<LoginRewardVM.OneReward> GenReward(DateTime time, LoginRewardInfo reward)
        {
            List<LoginRewardVM.OneReward> daysReward = new List<LoginRewardVM.OneReward>();
            int dayOfWeek = (int)time.DayOfWeek;
            for (int i = 0; i < _loginConfig.DayRewards.Count; ++i)
            {
                LoginRewardVM.OneReward oneReward;
                if (dayOfWeek > i)
                {
                    if (reward == null || !reward.GettedDays.Contains(i))
                    {
                        oneReward = new LoginRewardVM.OneReward
                        {
                            DayIndex = i,
                            RewardCoins = _loginConfig.DayRewards[i],
                            state = LoginRewardVM.OneRewardState.NotGetted
                        };

                    }
                    else
                    {
                        oneReward = new LoginRewardVM.OneReward
                        {
                            DayIndex = i,
                            RewardCoins = _loginConfig.DayRewards[i],
                            state = LoginRewardVM.OneRewardState.Getted
                        };

                    }

                }

                else if (dayOfWeek < i)
                {
                    oneReward = new LoginRewardVM.OneReward
                    {
                        DayIndex = i,
                        RewardCoins = _loginConfig.DayRewards[i],
                        state = LoginRewardVM.OneRewardState.Waitting
                    };
                }
                else
                {
                    if (reward == null || !reward.GettedDays.Contains(i))
                    {
                        oneReward = new LoginRewardVM.OneReward
                        {
                            DayIndex = i,
                            RewardCoins = _loginConfig.DayRewards[i],
                            state = LoginRewardVM.OneRewardState.Available
                        };
                    }
                    else
                    {
                        oneReward = new LoginRewardVM.OneReward
                        {
                            DayIndex = i,
                            RewardCoins = _loginConfig.DayRewards[i],
                            state = LoginRewardVM.OneRewardState.Getted
                        };
                    }
                }
                daysReward.Add(oneReward);

            }
            return daysReward;
        }
    
        public async Task<BodyResponse<RewardInfoVM>> Handle(GetLoginRewardCommand request, CancellationToken cancellationToken)
        {
            DateTime tnow = DateTime.Now;
            int dayOfWeek = (int)tnow.DayOfWeek;
            using (var locker = _redis.Loker(KeyGenHelper.GenUserWeekKey(tnow, request.Id, LoginRewardInfo.className)))
            {
                var rewardInfo = await _redis.GetLoginReward(tnow, request.Id);
                long rewardCoins = 0;
                if (rewardInfo == null || !rewardInfo.GettedDays.Contains(dayOfWeek))
                {
                    rewardCoins = _loginConfig.DayRewards[dayOfWeek];
                    if (rewardInfo == null)
                    {
                        rewardInfo = new LoginRewardInfo(request.Id, new List<int>());
                    }
                    rewardInfo.GettedDays.Add(dayOfWeek);
                    await _redis.SetUserLoginReward(tnow, rewardInfo);
                    _ = _mqBus.Publish(new AddMoneyMqCommand(request.Id, rewardCoins, 0, MoneyReson.LoginReward));
                }
                else
                {
                    return new BodyResponse<RewardInfoVM>(StatusCodeDefines.RewardGetted);
                }
                
                return new BodyResponse<RewardInfoVM>(StatusCodeDefines.Success, null, new RewardInfoVM(rewardCoins));
            }
        }
    }
}
