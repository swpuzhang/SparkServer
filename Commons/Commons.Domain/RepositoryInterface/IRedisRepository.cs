using Commons.Extenssions;
using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

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
            _key = key + "_l_o_k_e_r";
            _ownValue = Guid.NewGuid().ToString();
        }
        public void Lock(int ms = 3000)
        {
            while (!_redis.LockTake(_key, _ownValue, TimeSpan.FromMilliseconds(ms)))
            {
                Thread.Sleep(10);
            }
      
        }
        public bool TryLock(int ms = 3000)
        {
            return _redis.LockTake(_key, _ownValue, TimeSpan.FromMilliseconds(ms));
   
        
        }

        public async Task LockAsync(int ms = 3000)
        {
            while (!await _redis.LockTakeAsync(_key, _ownValue, TimeSpan.FromMilliseconds(ms)))
            {
                Thread.Sleep(10);
            }

        }
        public Task<bool> TryLockAsync(int ms = 3000)
        {
            return _redis.LockTakeAsync(_key, _ownValue, TimeSpan.FromMilliseconds(ms));


        }

        public void Dispose()
        {
           
            _redis.LockRelease(_key, _ownValue);
            
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
