using Commons.Domain.RepositoryInterface;
using GameSample.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameSample.Domain.RepositoryInterface
{
    public interface IGameSampleRedisRepository : IRedisRepository
    {
        Task SetUserRoomInfo(UserRoomInfo info);
        Task<UserRoomInfo> GetUserRoomInfo(long id);

        Task DeleteUserRoomInfo(long id);
    }
}
