using Commons.Infrastruct;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using Account.Domain.Models;

namespace Account.Infrastruct
{
    public class AccountContext : MongoContext
    {
        public IMongoCollection<AccountInfo> AccountInfos { get; private set; }
        public IMongoCollection<UserIdGenInfo> GenInfos { get; private set; }

        public IMongoCollection<LevelInfo> LevelInfos { get; private set; }

        public IMongoCollection<GameInfo> GameInfos { get; private set; }

        public AccountContext(IMongoSettings settings) : base(settings)
        {
            AccountInfos = base._database.GetCollection<AccountInfo>(typeof(AccountInfo).Name);
            GenInfos = base._database.GetCollection<UserIdGenInfo>(typeof(UserIdGenInfo).Name);
            LevelInfos = base._database.GetCollection<LevelInfo>(typeof(LevelInfo).Name);
            LevelInfos = base._database.GetCollection<LevelInfo>(typeof(LevelInfo).Name);
            GameInfos = base._database.GetCollection<GameInfo>(typeof(GameInfo).Name);
        }
    }
}
