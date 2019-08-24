using Commons.Infrastruct;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using Friend.Domain.Models;

namespace Friend.Infrastruct
{
    public class FriendContext : MongoContext
    {
        public IMongoCollection<FriendInfo> FriendInfos { get; private set; }
        public FriendContext(IMongoSettings settings) : base(settings)
        {
            FriendInfos = base._database.GetCollection<FriendInfo>(typeof(FriendInfo).Name);
        }
    }
}
