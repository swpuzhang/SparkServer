using Commons.Domain.RepositoryInterface;
using Commons.Extenssions;
using ServerLog.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerLog.Infrastruct
{
    public class ServerLogRedisRepository : RedisRepository, IServerLogRedisRepository
    {

        public ServerLogRedisRepository(RedisHelper redis) : base(redis)
        {

        }
    }
}
