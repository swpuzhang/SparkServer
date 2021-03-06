﻿using Account.Domain.Models;
using Account.Domain.RepositoryInterface;
using Commons.Infrastruct;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Account.Infrastruct
{
    public class UserIdGenRepository : MongoUserRepository<UserIdGenInfo>, IUserIdGenRepository
    {
        public UserIdGenRepository(AccountContext context) : base(context.GenInfos)
        {

        }

        public async Task<long> GenNewId()
        {
           
            var options = new FindOneAndUpdateOptions<UserIdGenInfo>
            {
                ReturnDocument = ReturnDocument.After
            };

            var update = Builders<UserIdGenInfo>.Update.Inc(nameof(UserIdGenInfo.UserId), 1);
            
            var ret = await _dbCol.FindOneAndUpdateAsync<UserIdGenInfo>(new BsonDocument(), update, options);
            return ret.UserId;
        }

        
    }
}
