using Commons.Domain.RepositoryInterface;
using Commons.Extenssions;
using Sample.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Infrastruct
{
    public class SampleRedisRepository : RedisRepository, ISampleRedisRepository
    {

        public SampleRedisRepository(RedisHelper redis) : base(redis)
        {

        }
    }
}
