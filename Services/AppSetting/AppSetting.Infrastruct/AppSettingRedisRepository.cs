using Commons.Domain.RepositoryInterface;
using Commons.Extenssions;
using AppSetting.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSetting.Infrastruct
{
    public class AppSettingRedisRepository : RedisRepository, IAppSettingRedisRepository
    {

        public AppSettingRedisRepository(RedisHelper redis) : base(redis)
        {

        }
    }
}
