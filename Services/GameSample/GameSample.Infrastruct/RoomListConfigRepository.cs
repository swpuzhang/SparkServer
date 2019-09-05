using Commons.Infrastruct;
using MongoDB.Driver;
using GameSample.Domain.Models;
using GameSample.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameSample.Infrastruct
{
    public class RoomListConfigRepository : IRoomListConfigRepository
    {
        protected readonly IMongoSettings _settings;
        IMongoClient _client;
        IMongoDatabase _db;
        IMongoCollection<RoomListConfig> _dbCol;
        public RoomListConfigRepository(IMongoSettings settings)
        {
            _settings = settings;
            _client = new MongoClient(_settings.ConnectionString);
            _db = _client.GetDatabase(_settings.DatabaseName);
            _dbCol = _db.GetCollection<RoomListConfig>(typeof(RoomListConfig).Name);
        }

        public  List<RoomListConfig> LoadConfig()
        {
           
            var configs = _dbCol.Find<RoomListConfig>(x => true);//.Project(Builders<LevelConfig>.
                                                             //Projection.Exclude("_id")).ToList();

            return  configs.Project<RoomListConfig>(Builders<RoomListConfig>.Projection.Exclude("_id")).ToList();
        }
        
    }
}
