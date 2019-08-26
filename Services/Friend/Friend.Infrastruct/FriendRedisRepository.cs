using Commons.Domain.RepositoryInterface;
using Commons.Extenssions;
using Commons.Extenssions.Defines;
using Friend.Domain.Models;
using Friend.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Infrastruct
{
    public class FriendRedisRepository : RedisRepository, IFriendRedisRepository
    {

        public FriendRedisRepository(RedisHelper redis) : base(redis)
        {
         
            
        }

        public async Task<FriendInfo> GetFriendInfo(long id)
        {
            FriendInfo info = new FriendInfo(id, await _redis.GetHashAllAsync<long, OneFriendInfo>
                (KeyGenHelper.GenUserKey(id, "FriendInfo")));
            return info;
        }

        public Task<List<long>> GetApplyInfo(long id)
        {
            return _redis.GetSetAllValueAsync<long>(KeyGenHelper.GenUserKey(id, "ApplyedFriendInfo"));
        }

        public Task SetFriendInfo(FriendInfo info)
        {
            if (info == null || info._friends.Count == 0)
            {
                //如果好友为空,插一条数据进去,写入redis,这样可以防止redis没数据,每次都从数据库读取
                var dic = new Dictionary<long, OneFriendInfo>() { { -1, new OneFriendInfo(-1, FriendTypes.None) } };
                return _redis.AddHashListAsync(KeyGenHelper.GenUserKey(info.Id, "FriendInfo"), dic, TimeSpan.FromDays(7));
            }
            return Task.WhenAll(_redis.AddHashListAsync(KeyGenHelper.GenUserKey(info.Id, "FriendInfo"),
                info._friends, TimeSpan.FromDays(7)));
        }

        public Task<bool> IsFriendInRedis(long id)
        {
            return  _redis.IsKeyExistAsync(KeyGenHelper.GenUserKey(id, "FriendInfo"), TimeSpan.FromDays(7));
        }

        public Task<long> GetFriendCount(long id)
        {
            return _redis.GetHashCountAsync(KeyGenHelper.GenUserKey(id, "FriendInfo"));
        }

        public Task<bool> IsAlreadyApplyed(long id, long friendId)
        {
            return _redis.IsSetContainsAsync(KeyGenHelper.GenUserKey(id, "ApplyedFriendInfo"), friendId.ToString());
        }

        public Task<long> GetApplyedFriendCount(long id)
        {
            return _redis.GetSetCountAsync(KeyGenHelper.GenUserKey(id, "ApplyedFriendInfo"));
        }

        public  Task AddApplyedFriend(long id, long friendId)
        {
             return Task.WhenAll(_redis.AddZsetValueAsync(KeyGenHelper.GenUserKey(id, "ApplyedFriendInfo"), friendId.ToString(),
                DateTime.Now.ToTimeStamp(), TimeSpan.FromDays(30)),
           _redis.DeleteZsetValueRangeAsync(
               KeyGenHelper.GenUserKey(id, "ApplyedFriendInfo"), 0, DateTime.Now.ToTimeStamp() - TimeSpan.FromDays(30).TotalSeconds));

        }

        public async Task AddFriend(long id, long friendId, FriendTypes type)
        {
            await Task.WhenAll(_redis.AddHashValueAsync(KeyGenHelper.GenUserKey(id, "FriendInfo"), 
                friendId.ToString(), new OneFriendInfo(friendId, type)),

                _redis.DeleteSetValueAsync(KeyGenHelper.GenUserKey(id, "ApplyedFriendInfo"),
                friendId.ToString()),

                _redis.DeleteSetValueAsync(KeyGenHelper.GenUserKey(friendId, "ApplyedFriendInfo"),
                friendId.ToString()),

                _redis.AddHashValueAsync(KeyGenHelper.GenUserKey(friendId, "FriendInfo"), id.ToString(), 
                new OneFriendInfo(id, type)));
        }

        public Task<bool> IgnoreApply(long id, long friendId)
        {
            return _redis.DeleteSetValueAsync(KeyGenHelper.GenUserKey(id, "ApplyedFriendInfo"), friendId.ToString());
        }

        public Task DeleteFriend(long id, long friendId)
        {
            return Task.WhenAll(_redis.DeleteHashValueAsync(KeyGenHelper.GenUserKey(id, "FriendInfo"), friendId.ToString()),
                _redis.DeleteHashValueAsync(KeyGenHelper.GenUserKey(friendId, "FriendInfo"), id.ToString()));
        }

        public Task<bool> IsPlatformDealed(string platform)
        {
            return _redis.IsSetContainsAsync(KeyGenHelper.GenKey("FriendPlatform"), platform);
        }

        public Task<OneFriendInfo> GetOneFriendInfo(long id, long friendId)
        {
            return _redis.GetHashValueAsync<OneFriendInfo>(KeyGenHelper.GenUserKey(id, "FriendInfo"), friendId.ToString());
        }
    }
}
