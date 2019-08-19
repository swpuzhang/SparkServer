using Commons.Domain.RepositoryInterface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commons.Infrastruct
{
    public class ConfigRepository<T> :  MongoOneContext<T>, IConfigRepository<T> where T : class
    {
        public ConfigRepository(IMongoSettings settings)
            : base(settings)
        {
        }


        public T LoadConfig()
        {

            var configs = _dbCol.Find<T>(x => true);//.Project(Builders<LevelConfig>.
                                                                       //Projection.Exclude("_id")).ToList();

            return configs.Project<T>(Builders<T>.Projection.Exclude("_id")).ToList().FirstOrDefault();
        }
    }

    public class ConfigListRepository<T> : MongoOneContext<T>, IConfigListRepository<T> where T : class
    {
        public ConfigListRepository(IMongoSettings settings)
            : base(settings)
        {
        }


        public List<T> LoadConfig()
        {

            var configs = _dbCol.Find<T>(x => true);//.Project(Builders<LevelConfig>.
                                                    //Projection.Exclude("_id")).ToList();
            return configs.Project<T>(Builders<T>.Projection.Exclude("_id")).ToList();
        }
    }
}
