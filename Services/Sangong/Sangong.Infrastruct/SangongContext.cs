using Commons.Infrastruct;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using Sangong.Domain.Models;

namespace Sangong.Infrastruct
{
    public class SangongContext : MongoContext
    {
        public IMongoCollection<SangongInfo> SangongInfos { get; private set; }
        public IMongoCollection<UserIdGenInfo> GenInfos { get; private set; }
        public SangongContext(IMongoSettings settings) : base(settings)
        {
            SangongInfos = base._database.GetCollection<SangongInfo>(typeof(SangongInfo).Name);
            GenInfos = base._database.GetCollection<UserIdGenInfo>(typeof(UserIdGenInfo).Name);
        }
    }
}
