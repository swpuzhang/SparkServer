using Commons.MqEvents;
using Dummy.MqCommands;
using MassTransit;
using Reward.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reward.WebApi.MqConsumers
{
    public class RewardConsumer :
        IConsumer<InviteFriendMqEvent>,
        IConsumer<RegistMqEvent>,
        IConsumer<GameLogMqCommand>
    {

        IRewardService _service;
        IActivityService _activityService;
        public RewardConsumer(IRewardService service, IActivityService activityService)
        {
            _service = service;
            _activityService = activityService;
        }

        public async Task Consume(ConsumeContext<InviteFriendMqEvent> context)
        {
            await _service.InvitedFriendReward(context.Message.Id, 
                context.Message.PlatformAccount, context.Message.Type);
        }

        public async Task Consume(ConsumeContext<RegistMqEvent> context)
        {
            await _service.InvitedFriendRegistered(context.Message.PlatformAccount, context.Message.Type);
        }

        public Task Consume(ConsumeContext<GameLogMqCommand> context)
        {
            return Task.CompletedTask;
           // await _activityService.AddActFromGamelog(context.Message);
        }
    }
}
