using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Reward.Application.ViewModels;
using Reward.Domain;
using Reward.Domain.Commands;
using Reward.Domain.Models;
using Reward.Domain.RepositoryInterface;
using AutoMapper;
using Commons.Domain.Bus;
using Commons.Domain.Models;
using Commons.Infrastruct;
using Reward.Domain.Events;
using Commons.Extenssions.Defines;

namespace Reward.Application.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        public ActivityService(IMapper mapper, IMediatorHandler bus)
        {
            _mapper = mapper;
            _bus = bus;
        }

        public Task<BodyResponse<RewardInfoVM>> GetGameActReward(long id, string activityId, string subId)
        {

            return _bus.SendCommand(new GetGameActRewardCommand(id, activityId, subId));
        }

        public async Task<BodyResponse<ActivityInfoVM>> QueryActivity(long id)
        {
            var playInfos = await _bus.SendCommand(new GameActivityCommand(id, ActivityTypes.PlayGame));
            var winInfos = await _bus.SendCommand(new GameActivityCommand(id, ActivityTypes.WinGame));
            return new BodyResponse<ActivityInfoVM>(StatusCodeDefines.Success, null,
                new ActivityInfoVM
                {
                    AllGameActivitys = playInfos,
                    AllWinActivitys = winInfos
                });
        }
    }
}
