using Commons.Infrastruct;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using Dummy.Domain.Models;

namespace Dummy.Infrastruct
{
    public class DummyContext : MongoContext
    {
        public IMongoCollection<DummyInfo> DummyInfos { get; private set; }
        public DummyContext(IMongoSettings settings) : base(settings)
        {
            DummyInfos = base._database.GetCollection<DummyInfo>(typeof(DummyInfo).Name);
        }
    }
}
