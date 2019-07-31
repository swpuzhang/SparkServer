using Commons.Infrastruct;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using Sample.Domain.Models;

namespace Sample.Infrastruct
{
    public class SampleContext : MongoContext
    {
        public IMongoCollection<SampleInfo> SampleInfos { get; private set; }
        public SampleContext(IMongoSettings settings) : base(settings)
        {
            SampleInfos = base._database.GetCollection<SampleInfo>(typeof(SampleInfo).Name);
        }
    }
}
