using Friend.Domain;
using Friend.Domain.Models;
using Commons.Infrastruct;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Friend.Domain.RepositoryInterface;
using Commons.Extenssions.Defines;

namespace Friend.Infrastruct
{
    public class FriendInfoRepository : MongoUserRepository<FriendInfo>, IFriendInfoRepository
    {
        
        public FriendInfoRepository(FriendContext context) : base(context.FriendInfos)
        {

        }

        public Task AddFriend(long id, long friendId, FriendTypes type)
        {
            //Builders<FriendInfo>.Update.Set(x => x.Id == id && x._friends.TryGetValue(friendId, out var value))
            return Task.WhenAll(_dbCol.UpdateOneAsync(x => x.Id == id,
                Builders<FriendInfo>.Update.AddToSet
                (x => x._friends.Select( y => y.Value), new OneFriendInfo(friendId, type))),
                _dbCol.UpdateOneAsync(x => x.Id == friendId,
                Builders<FriendInfo>.Update.AddToSet
                (x => x._friends.Select(y => y.Value), new OneFriendInfo(id, type))));
        }

        public Task DeleteFriend(long id, long friendId)
        {
            
            return Task.WhenAll(_dbCol.UpdateOneAsync(x => x.Id == id,
                Builders<FriendInfo>.Update.PullFilter(x => x._friends.Select(y => y.Value), x => x.FriendId == friendId)),
                _dbCol.UpdateOneAsync(x => x.Id == friendId,
                Builders<FriendInfo>.Update.PullFilter<OneFriendInfo>(x => x._friends.Select(y => y.Value), x => x.FriendId == id)));

        }

        public async Task UpdateFriend(long id, long friendId, FriendTypes type)
        {
            await DeleteFriend(id, friendId);
            await AddFriend(id, friendId, type);
        }
    }
}
