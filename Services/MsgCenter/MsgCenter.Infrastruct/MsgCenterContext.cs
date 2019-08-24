using Commons.Infrastruct;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using MsgCenter.Domain.Models;

namespace MsgCenter.Infrastruct
{
    /*public class MsgCenterContext : MongoContext
    {
        public IMongoCollection<MsgCenterInfo> MsgCenterInfos { get; private set; }
        public MsgCenterContext(IMongoSettings settings) : base(settings)
        {
            MsgCenterInfos = base._database.GetCollection<MsgCenterInfo>(typeof(MsgCenterInfo).Name);
        }
    }*/
}
