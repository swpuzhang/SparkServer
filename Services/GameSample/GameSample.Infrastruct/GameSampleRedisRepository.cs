using Commons.Domain.RepositoryInterface;
using Commons.Extenssions;
using GameSample.Domain.Models;
using GameSample.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameSample.Infrastruct
{
    public class GameSampleRedisRepository : RedisRepository, IGameSampleRedisRepository
    {

        public GameSampleRedisRepository(RedisHelper redis) : base(redis)
        {

        }

        public Task<UserRoomInfo> GetUserRoomInfo(long id)
        {
            return _redis.GetStringAsync<UserRoomInfo>(KeyGenHelper.GenUserKey(id, UserRoomInfo.className));
        }

        public Task SetUserRoomInfo(UserRoomInfo info)
        {
            return _redis.SetStringAsync<UserRoomInfo>(KeyGenHelper.GenUserKey(info.Id, UserRoomInfo.className), 
                info, TimeSpan.FromHours(1));
        }

        public Task DeleteUserRoomInfo(long id)
        {
            return _redis.DeleteKeyAsync(KeyGenHelper.GenUserKey(id, UserRoomInfo.className));
        }
    }
}
