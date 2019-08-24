using Friend.Domain.Models;
using Commons.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Domain.RepositoryInterface
{
    public interface IFriendInfoRepository : IMongoUserRepository<FriendInfo>
    {
        Task AddFriend(long id, long friendId, int type);
        Task DeleteFriend(long id, long friendId);

        Task UpdateFriend(long id, long friendId, int type);
    }
}
