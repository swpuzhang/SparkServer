using Commons.Domain.RepositoryInterface;
using Reward.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reward.Domain.RepositoryInterface
{
    public interface IRewardRedisRepository : IRedisRepository
    {
        Task SetUserRegiserReward(RegisterRewardInfo info);
        Task<RegisterRewardInfo> GetUserRegiserReward(long id);
        Task<LoginRewardInfo> GetLoginReward(DateTime date, long id);

        Task SetUserLoginReward(DateTime date, LoginRewardInfo info);
        Task<BankruptcyInfo> GetBankruptcyInfo(DateTime date, long id);
        Task SetBankruptcyInfo(DateTime date, BankruptcyInfo info);

        Task SetInviteFriend(long id, string platform, int type);
        Task RemovInviteFriend(List<long> allInviter, string platform, int type);
        Task<List<long>> GetInviter(string platform, int type);
    }
}
