using Commons.Domain.RepositoryInterface;
using Commons.Extenssions;
using Sangong.Domain.Models;
using Sangong.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sangong.Infrastruct
{
    public class SangongRedisRepository : RedisRepository, ISangongRedisRepository
    {

        public SangongRedisRepository(RedisHelper redis) : base(redis)
        {

        }

        public Task<UserRoomInfo> GetUserRoomInfo(long id)
        {
            return _redis.GetStringAsync<UserRoomInfo>(KeyGenHelper.GenUserKey(id, UserRoomInfo.className));
        }

        public Task SetUserRoomInfo(UserRoomInfo info)
        {
            return _redis.SetStringAsync<UserRoomInfo>(KeyGenHelper.GenUserKey(info.Id, UserRoomInfo.className), info);
        }
    }
}
