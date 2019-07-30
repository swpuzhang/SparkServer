using Commons.Extenssions;
using Money.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Infrastruct
{
    public class MoneyRedisRepository : IMoneyRedisRepository
    {

        private RedisHelper _redis;

        public MoneyRedisRepository(RedisHelper redis)
        {
            _redis = redis;
        }
    }
}
