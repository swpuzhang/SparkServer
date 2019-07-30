using Account.Domain.Models;
using Account.Domain.RepositoryInterface;
using Commons.Domain.RepositoryInterface;
using Commons.Extenssions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Account.Infrastruct
{
    public class AccountRedisRepository : RedisRepository, IAccountRedisRepository
    {
        
        public AccountRedisRepository(RedisHelper redis) : base(redis)
        {
           
        }
        public Task<LoginCheckInfo> GetLoginCheckInfo(string platformAccount)
        {
            return _redis.GetStringAsync<LoginCheckInfo>(KeyGenHelper.GenKey(platformAccount, 
                typeof(LoginCheckInfo).Name));
        }

        public Task SetAccountInfo(AccountInfo info)
        {
            _redis.SetStringAsync<AccountInfo>(KeyGenHelper.GenUserKey(info.Id,
                typeof(AccountInfo).Name), info, TimeSpan.FromDays(7));
            return Task.CompletedTask;
        }

        public Task SetLoginCheckInfo(string account, AccountInfo info)
        {
            _redis.SetStringNoWait<LoginCheckInfo>(KeyGenHelper.GenKey(account, typeof(LoginCheckInfo).Name),
                new LoginCheckInfo(info.Id, info.PlatformAccount, info.Type),
                TimeSpan.FromDays(7));
            return Task.CompletedTask;
        }

        public Task<AccountInfo> GetAccountInfo(long id)
        {
            return _redis.GetStringAsync<AccountInfo>(KeyGenHelper.GenUserKey(id,
                typeof(AccountInfo).Name));
        }

        public Task<LevelInfo> GetLevelInfo(long id)
        {
            return _redis.GetStringAsync<LevelInfo>(KeyGenHelper
                .GenUserKey(id, LevelInfo.ClassName));
        }

        public Task SetLevelInfo(LevelInfo info)
        {
            _redis.SetStringNoWait<LevelInfo>(KeyGenHelper
                .GenUserKey(info.Id, LevelInfo.ClassName), info, TimeSpan.FromDays(7));
            return Task.CompletedTask;
        }

        public Task<GameInfo> GetGameInfo(long id)
        {
            return _redis.GetStringAsync<GameInfo>(KeyGenHelper.GenUserKey(id, GameInfo.ClassName));
        }

        public Task SetGameInfo(GameInfo info)
        {
            _redis.SetStringNoWait<GameInfo>(KeyGenHelper
                .GenUserKey(info.Id, GameInfo.ClassName), info, TimeSpan.FromDays(7));
            return Task.CompletedTask;
        }
    }
}
