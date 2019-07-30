using Account.Domain.Models;
using Account.Domain.RepositoryInterface;
using Commons.Infrastruct;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Account.Infrastruct
{
    public class LevelConfigRepository : IlevelConfigRepository
    {
        protected readonly IMongoSettings _settings;
        public LevelConfigRepository()
        {
            
        }
       
        public List<LevelConfig> LoadLevelConfig()
        {
            IMongoClient client = new MongoClient(_settings.ConnectionString);
            IMongoDatabase db = client.GetDatabase(_settings.DatabaseName);
            var dbCol = db.GetCollection<LevelConfig>(typeof(LevelConfig).Name);
            var configs = dbCol.Find<LevelConfig>(x => true);

            return configs.ToList();
        }
    }
}
