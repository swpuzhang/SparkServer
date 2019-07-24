﻿using MongoDB.Driver;
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
}
