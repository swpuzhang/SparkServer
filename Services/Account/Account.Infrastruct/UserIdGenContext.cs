using Commons.Infrastruct;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using Account.Domain.Models;

namespace Account.Infrastruct
{
    public class UserIdGenContext : MongoContext
    {
        public IMongoCollection<UserIdGenInfo> GenInfos { get; private set; }
        public UserIdGenContext(IMongoSettings settings) : base(settings)
        {
            GenInfos = base._database.GetCollection<UserIdGenInfo>(typeof(UserIdGenInfo).Name);
        }
    }
}
