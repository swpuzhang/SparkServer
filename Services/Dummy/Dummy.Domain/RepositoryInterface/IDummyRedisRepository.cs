using Commons.Domain.RepositoryInterface;
using Dummy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Domain.RepositoryInterface
{
    public interface IDummyRedisRepository : IRedisRepository
    {
        Task SetUserRoomInfo(UserRoomInfo info);
        Task<UserRoomInfo> GetUserRoomInfo(long id);

        Task DeleteUserRoomInfo(long id);
    }
}
