﻿using Commons.Domain;
using Commons.Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Commons.Domain.RepositoryInterface;

namespace Commons.Infrastruct
{
    public class MongoUserOneRepository<TEntity> : MongoOneContext<TEntity>, IMongoUserRepository<TEntity> where TEntity : UserEntity
    {
        public MongoUserOneRepository(IMongoSettings settings)
            : base(settings)
        {
        }
        public void Add(TEntity obj)
        {
            _dbCol.InsertOne(obj);
        }

        public async Task AddAsync(TEntity obj)
        {
            await _dbCol.InsertOneAsync(obj);
        }

        public List<TEntity> GetAll()
        {
            return _dbCol.Find(e => true).ToList();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var all = await _dbCol.FindAsync(e => true);

            return await all.ToListAsync();
        }

        public TEntity GetById(Int64 id)
        {
            return _dbCol.Find<TEntity>(e => e.Id == id).FirstOrDefault();

        }

        public async Task<TEntity> GetByIdAsync(Int64 id)
        {
            var one = await _dbCol.FindAsync<TEntity>(e => e.Id == id);
            return await one.FirstOrDefaultAsync();

        }

        public void Remove(Int64 id)
        {
            _dbCol.DeleteOne<TEntity>(e => e.Id == id);
        }

        public async Task RemoveAsync(Int64 id)
        {
            await _dbCol.DeleteOneAsync<TEntity>(e => e.Id == id);
        }

        public void Replace(TEntity obj)
        {
            _dbCol.ReplaceOne(e => e.Id == obj.Id, obj);

        }

        public async Task ReplaceAsync(TEntity obj)
        {
            await _dbCol.ReplaceOneAsync<TEntity>(e => e.Id == obj.Id, obj);
        }

        public async Task ReplaceAndAddAsync(TEntity obj)
        {
            var options = new UpdateOptions
            {
                IsUpsert = true
            };
            await _dbCol.ReplaceOneAsync<TEntity>(e => e.Id == obj.Id, obj, options);
        }

        public async Task UpdateAsync(TEntity obj)
        {
            BsonDocument bs = new BsonDocument("$set", obj.ToBsonDocument<TEntity>());
            await _dbCol.UpdateOneAsync<TEntity>(e => e.Id == obj.Id, bs);
        }

        public async Task<TEntity> FindAndAdd(long id, TEntity info)
        {
            var options = new FindOneAndReplaceOptions<TEntity>
            {
                ReturnDocument = ReturnDocument.After,
                IsUpsert = true
            };
            return await _dbCol.FindOneAndReplaceAsync<TEntity>(x => x.Id == id, info, options);
        }
    }

    public class MongoUserRepository<TEntity> : IMongoUserRepository<TEntity> where TEntity : UserEntity
    {
   
        protected readonly IMongoCollection<TEntity> _dbCol;
        public MongoUserRepository(IMongoCollection<TEntity> dbCol)
        {
            _dbCol = dbCol;
        }
        public void Add(TEntity obj)
        {
            _dbCol.InsertOne(obj);
        }

        public async Task AddAsync(TEntity obj)
        {
           await _dbCol.InsertOneAsync(obj);
        }

        public List<TEntity> GetAll()
        {
            return _dbCol.Find(e => true).ToList();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var all = await _dbCol.FindAsync(e => true);

            return await all.ToListAsync();
        }

        public TEntity GetById(Int64 id)
        {
            return _dbCol.Find<TEntity>(e => e.Id == id).FirstOrDefault();
            
        }

        public async Task<TEntity> GetByIdAsync(Int64 id)
        {
            var one = await _dbCol.FindAsync<TEntity>(e => e.Id == id);
            return await one.FirstOrDefaultAsync();
 
        }

        public void Remove(Int64 id)
        {
            _dbCol.DeleteOne<TEntity>(e => e.Id == id);
        }

        public async Task RemoveAsync(Int64 id)
        {
            await _dbCol.DeleteOneAsync<TEntity>(e => e.Id == id);
        }

        public void Replace(TEntity obj)
        {
            _dbCol.ReplaceOne(e => e.Id == obj.Id, obj);
            
        }

        public async Task ReplaceAsync(TEntity obj)
        {
            await _dbCol.ReplaceOneAsync<TEntity>(e => e.Id == obj.Id, obj);
        }

        public async Task ReplaceAndAddAsync(TEntity obj)
        {
            var options = new UpdateOptions
            {
                IsUpsert = true
            };
            await _dbCol.ReplaceOneAsync<TEntity>(e => e.Id == obj.Id, obj, options);
        }

        public async Task UpdateAsync(TEntity obj)
        {
            BsonDocument bs = new BsonDocument("$set", obj.ToBsonDocument<TEntity>());
            await _dbCol.UpdateOneAsync<TEntity>(e => e.Id == obj.Id, bs);
        }

        public async Task<TEntity> FindAndAdd(long id, TEntity info)
        {
            var options = new FindOneAndReplaceOptions<TEntity>
            {
                ReturnDocument = ReturnDocument.After,
                IsUpsert = true
            };
            return await _dbCol.FindOneAndReplaceAsync<TEntity>(x => x.Id == id, info, options);
        }
    }
}
