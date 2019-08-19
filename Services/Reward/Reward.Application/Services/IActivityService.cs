using Reward.Application.ViewModels;
using Reward.Domain.Models;
using Commons.Domain.Models;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reward.Application.Services
{
    public interface IActivityService
    {
        Task<BodyResponse<ActivityInfoVM>> QueryActivity(long id);
        Task<BodyResponse<RewardInfoVM>> GetGameActReward(long id, string activityId, string subId);
        
    }
}