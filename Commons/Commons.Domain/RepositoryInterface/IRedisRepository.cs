using Commons.Extenssions;
using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;
using System.Threading;

namespace Commons.Domain.RepositoryInterface
{
    public interface IRedisRepository
    {
        ReidsLockGuard Loker(string key);
    }

    public class ReidsLockGuard :  IDisposable
    {
        private readonly RedisHelper _redis;
        private readonly string _key;
        private readonly string _ownValue;
        public ReidsLockGuard(RedisHelper redis, string key)
        {
            _redis = redis;
            _key = key;
            _ownValue = Guid.NewGuid().ToString();
        }
        public void Lock(int ms = 3000)
        {
            while (!_redis.LockTake(_key, _ownValue, TimeSpan.FromMilliseconds(ms)))
            {
                Thread.Sleep(10);
            }
        }
        public void Dispose()
        {
            _redis.LockRelease(_key, _ownValue);
            GC.SuppressFinalize(this);
        }
    }
    public class RedisRepository : IRedisRepository
    {
        protected readonly RedisHelper _redis;

        public RedisRepository(RedisHelper redis)
        {
            _redis = redis;
        }

        public ReidsLockGuard Loker(string key)
        {
            return new ReidsLockGuard(_redis, key);
        }
    }
}
