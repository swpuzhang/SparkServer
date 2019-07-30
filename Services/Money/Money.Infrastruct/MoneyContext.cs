using Commons.Infrastruct;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using Money.Domain.Models;

namespace Money.Infrastruct
{
    public class MoneyContext : MongoContext
    {
        public IMongoCollection<MoneyInfo> MoneyInfos { get; private set; }
        public IMongoCollection<UserIdGenInfo> GenInfos { get; private set; }
        public MoneyContext(IMongoSettings settings) : base(settings)
        {
            MoneyInfos = base._database.GetCollection<MoneyInfo>(typeof(MoneyInfo).Name);
            GenInfos = base._database.GetCollection<UserIdGenInfo>(typeof(UserIdGenInfo).Name);
        }
    }
}
