using Commons.Infrastruct;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using GameSample.Domain.Models;

namespace GameSample.Infrastruct
{
    public class GameSampleContext : MongoContext
    {
        public IMongoCollection<GameSampleInfo> GameSampleInfos { get; private set; }
        public GameSampleContext(IMongoSettings settings) : base(settings)
        {
            GameSampleInfos = base._database.GetCollection<GameSampleInfo>(typeof(GameSampleInfo).Name);
        }
    }
}
