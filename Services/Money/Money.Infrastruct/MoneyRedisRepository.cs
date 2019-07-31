using Commons.Domain.RepositoryInterface;
using Commons.Extenssions;
using Money.Domain.Models;
using Money.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Money.Infrastruct
{
    public class MoneyRedisRepository : RedisRepository, IMoneyRedisRepository
    {


        public MoneyRedisRepository(RedisHelper redis) : base(redis)
        {
            
        }

        public Task<MoneyInfo> GetMoney(long id)
        {
            return _redis.GetStringAsync<MoneyInfo>(KeyGenHelper.GenUserKey(id, MoneyInfo.ClassName));
        }

        public Task SetMoney(long id, MoneyInfo info)
        {
             _redis.SetStringAsync<MoneyInfo>(KeyGenHelper.GenUserKey(id, MoneyInfo.ClassName),
                 info, TimeSpan.FromDays(7));
            return Task.CompletedTask;
        }
    }
}
