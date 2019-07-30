using Commons.Extenssions;
using Sangong.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sangong.Infrastruct
{
    public class SangongRedisRepository : ISangongRedisRepository
    {

        private RedisHelper _redis;

        public SangongRedisRepository(RedisHelper redis)
        {
            _redis = redis;
        }
    }
}
