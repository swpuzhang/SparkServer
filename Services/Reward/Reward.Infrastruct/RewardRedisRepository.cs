using Commons.Domain.RepositoryInterface;
using Commons.Extenssions;
using Reward.Domain.Models;
using Reward.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Reward.Infrastruct
{
    public class RewardRedisRepository : RedisRepository, IRewardRedisRepository
    {

        public RewardRedisRepository(RedisHelper redis) : base(redis)
        {

        }

        public Task<RegisterRewardInfo> GetUserRegiserReward(long id)
        {
            return _redis.GetStringAsync<RegisterRewardInfo>(KeyGenHelper.GenUserKey(id, RegisterRewardInfo.className));
        }

        public Task SetUserRegiserReward(RegisterRewardInfo info )
        {
            return _redis.SetStringAsync(KeyGenHelper.GenUserKey(info.Id, RegisterRewardInfo.className), info, TimeSpan.FromDays(5));
        }

        public Task<LoginRewardInfo> GetLoginReward(DateTime date, long id)
        {
            return _redis.GetStringAsync<LoginRewardInfo>
                (KeyGenHelper.GenUserWeekKey(date, id, LoginRewardInfo.className));
        }

        public Task SetUserLoginReward(DateTime date, LoginRewardInfo info)
        {
            return _redis.SetStringAsync(KeyGenHelper.GenUserWeekKey(date, info.Id, LoginRewardInfo.className), info, TimeSpan.FromDays(8));
        }

        public Task<BankruptcyInfo> GetBankruptcyInfo(DateTime date, long id)
        {
            return _redis.GetStringAsync<BankruptcyInfo>
                (KeyGenHelper.GenUserDayKey(date, id, BankruptcyInfo.className));
        }

        public Task SetBankruptcyInfo(DateTime date, BankruptcyInfo info)
        {
            
            return _redis.SetStringAsync(KeyGenHelper.GenUserDayKey
                (date, info.Id, BankruptcyInfo.className), info, TimeSpan.FromDays(1));
        }

        public  Task SetInviteFriend(long id, string platform, int type)
        {
            var t1 = _redis.AddZsetValueAsync(KeyGenHelper.GenKey
                 (platform, "Invited"), id.ToString(), 
                 DateTime.Now.ToTimeStamp(), TimeSpan.FromDays(30));
            var t2 = _redis.DeleteZsetValueRangeAsync(KeyGenHelper.GenKey
                 (platform, "Invited"), 0, DateTime.Now.ToTimeStamp() - TimeSpan.FromDays(30).TotalSeconds);
            var t3 = _redis.AddZsetValueAsync(KeyGenHelper.GenUserKey
                (id, "Inviter"), platform,
                DateTime.Now.ToTimeStamp(), TimeSpan.FromDays(30));
            var t4 = _redis.DeleteZsetValueRangeAsync(KeyGenHelper.GenUserKey
                (id, "Inviter"), 0, DateTime.Now.ToTimeStamp() - TimeSpan.FromDays(30).TotalSeconds);
            return Task.WhenAll(t1, t2, t3, t4);
        }

        public async Task<List<long>> GetInviter(string platform, int type)
        {

            var allInviter = await _redis.GetZsetAllKeyAsync(KeyGenHelper.GenKey
                 (platform, "Invited"));
            return allInviter.ConvertAll<long>(x => Convert.ToInt64(x));
          
        }

        public Task RemovInviteFriend(List<long> allInviter, string platform, int type)
        {
            List<Task> allTasks = new List<Task>();
            allTasks.Add(_redis.DeleteKeyAsync(KeyGenHelper.GenKey
                 (platform, "Invited")));
            foreach (var oneInviter in allInviter)
            {
                allTasks.Add(_redis.DeleteZsetValueAsync(KeyGenHelper.GenUserKey
                (oneInviter, "Inviter"), platform));
            }
            return Task.WhenAll(allTasks);
        }
    }
}
