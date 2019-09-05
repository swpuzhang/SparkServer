using Commons.Domain.RepositoryInterface;
using Commons.Extenssions;
using Dummy.Domain.Models;
using Dummy.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Infrastruct
{
    public class DummyRedisRepository : RedisRepository, IDummyRedisRepository
    {

        public DummyRedisRepository(RedisHelper redis) : base(redis)
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
