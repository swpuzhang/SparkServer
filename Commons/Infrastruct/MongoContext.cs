using MongoDB.Driver;
using System;

namespace Commons.Infrastruct
{
    public class MongoContext
    {
        protected IMongoSettings _settings;
        protected IMongoClient _client;
        protected IMongoDatabase _database;
        public MongoContext(IMongoSettings settings)
        {
            _settings = settings;
            _client = new MongoClient(settings.ConnectionString);
            _database = _client.GetDatabase(settings.DatabaseName);
            
        }

        
    }

    public class MongoOneContext<TEntity> where TEntity : class
    {
        protected IMongoSettings _settings;
        protected IMongoClient _client;
        protected IMongoDatabase _database;
        protected readonly IMongoCollection<TEntity> _dbCol;
        public MongoOneContext(IMongoSettings settings)
        {
            _settings = settings;
            _client = new MongoClient(settings.ConnectionString);
            _database = _client.GetDatabase(settings.DatabaseName);
            _dbCol = _database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

    }
}
