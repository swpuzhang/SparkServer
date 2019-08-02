using Commons.Domain.RepositoryInterface;
using Sangong.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sangong.Domain.RepositoryInterface
{
    public interface ISangongRedisRepository : IRedisRepository
    {
        Task SetUserRoomInfo(UserRoomInfo info);
        Task<UserRoomInfo> GetUserRoomInfo(long id);
    }
}
