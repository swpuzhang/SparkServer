using Commons.Domain.RepositoryInterface;
using Friend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Domain.RepositoryInterface
{
    public interface IFriendRedisRepository : IRedisRepository
    {
        Task<FriendInfo> GetFriendInfo(long id);
        Task<List<long>> GetApplyInfo(long id);
        Task SetFriendInfo(FriendInfo info);
        Task<bool> IsFriendInRedis(long id);
        Task<bool> IsAlreadyApplyed(long id, long friendId);
        Task AddApplyedFriend(long id, long friendId);
        Task<long> GetApplyedFriendCount(long id);
        Task<long> GetFriendCount(long id);
        Task AddFriend(long id, long friendId, int type);
        Task<bool> IgnoreApply(long id, long friendId);
        Task DeleteFriend(long id, long friendId);
        Task<bool> IsPlatformDealed(string platform);

    }
}
