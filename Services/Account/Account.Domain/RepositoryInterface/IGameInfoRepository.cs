using Account.Domain.Models;
using Commons.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.RepositoryInterface
{
    public interface IGameInfoRepository : IMongoUserRepository<GameInfo>
    {
        
    }
}
