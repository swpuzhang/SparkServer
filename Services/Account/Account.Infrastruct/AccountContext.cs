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
        public AccountContext(IMongoSettings settings) : base(settings)
        {
            AccountInfos = base._database.GetCollection<AccountInfo>(typeof(AccountInfo).Name);
        }
    }
}
