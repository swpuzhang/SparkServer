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
    public interface IRewardService
    {
        Task<BodyResponse<RegisterRewardVM>> QueryRegisterReward(long id);
        Task<BodyResponse<RewardInfoVM>> GetRegisterReward(long id);
        Task<BodyResponse<LoginRewardVM>> QueryLoginReward(long id);
        Task<BodyResponse<RewardInfoVM>> GetLoginReward(long id);
        Task<BodyResponse<BankruptcyInfoVM>> QueryBankruptcy(long id);
        Task<BodyResponse<RewardInfoVM>> GetBankruptcy(long id);
        Task InvitedFriendReward(long id, string platform, int type);
        Task InvitedFriendRegistered(string platform, int type);
    }
}