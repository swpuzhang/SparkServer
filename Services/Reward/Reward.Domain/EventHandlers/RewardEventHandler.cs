using AutoMapper;
using MassTransit;
using MediatR;
using Reward.Domain.Events;
using Reward.Domain.Models;
using Reward.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Commons.MqCommands;

namespace Reward.Domain.EventHandlers
{
    public class RewardEventHandler : 
        INotificationHandler<InvitedFriendEvent>,
        INotificationHandler<InvitedFriendRegisterdEvent>
    {
        private readonly IRewardRedisRepository _redis;
        private readonly InviteRewardConfig _inviteConfig;
        private readonly IBusControl _mqBus;

        public RewardEventHandler(IRewardRedisRepository redis, InviteRewardConfig inviteConfig, IBusControl mqBus)
        {
            _redis = redis;
            _inviteConfig = inviteConfig;
            _mqBus = mqBus;
        }

        public Task Handle(InvitedFriendEvent notification, CancellationToken cancellationToken)
        {
            return _redis.SetInviteFriend(notification.Id, notification.PlatformAccount, notification.Type);
        }

        public async Task Handle(InvitedFriendRegisterdEvent notification, CancellationToken cancellationToken)
        {

            var allInviters = await _redis.GetInviter(notification.PlatformAccount, notification.Type);
            if (allInviters == null || allInviters.Count == 0)
            {
                return;
            }
            long rewardCoins = _inviteConfig.InviteRewards;
            foreach (var oneInviter in allInviters)
            {
                _ = _mqBus.Publish(new AddMoneyMqCommand(oneInviter, rewardCoins, 0, Commons.Extenssions.Defines.MoneyReson.Invite));
            }
            await _redis.RemovInviteFriend(allInviters, notification.PlatformAccount, notification.Type);
        }
    }
}
