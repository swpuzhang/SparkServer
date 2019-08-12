using Commons.Infrastruct;
using MongoDB.Driver;
using Sangong.Domain.Models;
using Sangong.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sangong.Infrastruct
{
    public class ConfigRepository : IConfigRepository
    {
        protected readonly IMongoSettings _settings;
        IMongoClient _client;
        IMongoDatabase _db;
        IMongoCollection<CoinsRangeConfig> _dbCol;
        public ConfigRepository(IMongoSettings settings)
        {
            _settings = settings;
            _client = new MongoClient(_settings.ConnectionString);
            _db = _client.GetDatabase(_settings.DatabaseName);
            _dbCol = _db.GetCollection<CoinsRangeConfig>(typeof(CoinsRangeConfig).Name);
        }

        public  List<CoinsRangeConfig> LoadCoinsRangeConfig()
        {
           
            var configs = _dbCol.Find<CoinsRangeConfig>(x => true);//.Project(Builders<LevelConfig>.
                                                             //Projection.Exclude("_id")).ToList();

            return  configs.Project<CoinsRangeConfig>(Builders<CoinsRangeConfig>.Projection.Exclude("_id")).ToList();
        
        }
        
    }
}
