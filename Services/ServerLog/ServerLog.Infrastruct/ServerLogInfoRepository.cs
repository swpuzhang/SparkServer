using ServerLog.Domain;
using ServerLog.Domain.Models;
using Commons.Infrastruct;
using MongoDB.Driver;
using Commons.Extenssions;
using ServerLog.Domain.RepositoryInterface;
using System;
using System.Threading.Tasks;

namespace ServerLog.Infrastruct
{
    public class GameLogInfoRepository : MongoOneContext<GameLogInfo>, IGameLogInfoRepository
    {
        
        public GameLogInfoRepository(IMongoSettings settings) 
            : base(settings, $"GameLogInfo_{DateTime.Now.ToNormalDay()}")
        {

        }

        public Task InsertLog(GameLogInfo logInfo)
        {
            return _dbCol.InsertOneAsync(logInfo);
        }
    }

    public class MoneyLogInfoRepository : MongoOneContext<MoneyLogInfo>, IMoneyLogInfoRepository
    {

        public MoneyLogInfoRepository(IMongoSettings settings)
            : base(settings, $"MoneyLogInfo_{DateTime.Now.ToNormalDay()}")
        {

        }

        public Task InsertLog(MoneyLogInfo logInfo)
        {
            return _dbCol.InsertOneAsync(logInfo);
        }
    }
}
