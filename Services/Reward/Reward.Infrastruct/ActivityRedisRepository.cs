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
    public class ActivityRedisRepository : RedisRepository, IActivityRedisRepository
    {

        public ActivityRedisRepository(RedisHelper redis) : base(redis)
        {

        }

        public async Task<OneGameActivityInfo> GetGameActivity(DateTime time, long id, string activityId)
        {
            Dictionary<string, GameSubActInfo> dic = await _redis.HashGetAllAsync<string, GameSubActInfo>
                (KeyGenHelper.GenUserDayKey(time, id, "GameActivity",  activityId));
            return new OneGameActivityInfo(activityId, dic);
        }

        public async Task<GameSubActInfo> GetGameActProgress(DateTime time, long id, string activityId, string subId)
        {
            GameSubActInfo subAct = await _redis.GetHashValueAsync<GameSubActInfo>
                (KeyGenHelper.GenUserDayKey(time, id, "GameActivity", activityId), subId);
            return subAct;
        }

        public Task SetGameActProgress(DateTime time, long id, string activityId, 
            string subId, GameSubActInfo subAct)
        {

            return _redis.SetHashValueAsync(KeyGenHelper.GenUserDayKey(time, id,
                "GameActivity", activityId), subId, subAct);
        }
    }
}
