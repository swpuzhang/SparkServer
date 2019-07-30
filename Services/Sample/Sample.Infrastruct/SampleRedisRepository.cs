using Commons.Extenssions;
using Sample.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Infrastruct
{
    public class SampleRedisRepository : ISampleRedisRepository
    {

        private RedisHelper _redis;

        public SampleRedisRepository(RedisHelper redis)
        {
            _redis = redis;
        }
    }
}
