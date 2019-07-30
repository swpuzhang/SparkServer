using Account.Domain.Models;
using Account.Domain.RepositoryInterface;
using Commons.Infrastruct;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Account.Infrastruct
{
    public class GameInfoRepository : MongoUserRepository<GameInfo>, IGameInfoRepository
    {
        public GameInfoRepository(AccountContext context) : base(context.GameInfos)
        {
            
        }

        
    }
}
