using Money.Domain.Models;
using Money.Domain.RepositoryInterface;
using Commons.Infrastruct;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Money.Infrastruct
{
    public class UserIdGenRepository : MongoUserRepository<UserIdGenInfo>, IUserIdGenRepository
    {
        public UserIdGenRepository(MoneyContext context) : base(context.GenInfos)
        {

        }

        public async Task<long> GenNewId()
        {
           
            var options = new FindOneAndUpdateOptions<UserIdGenInfo>
            {
                ReturnDocument = ReturnDocument.After
            };

            var update = Builders<UserIdGenInfo>.Update.Inc(nameof(UserIdGenInfo.UserId), 1);
            _dbCol.Find(e => true);
            var ret = await _dbCol.FindOneAndUpdateAsync<UserIdGenInfo>(new BsonDocument(), update, options);
            return ret.UserId;
        }

        
    }
}
