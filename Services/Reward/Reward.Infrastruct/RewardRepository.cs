using Commons.Infrastruct;
using MongoDB.Driver;
using Reward.Domain.Models;
using Reward.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reward.Infrastruct
{
    public class RegisterRewardRepository : MongoUserOneRepository<RegisterRewardInfo>, IRegisterRewardRepository
    {
        public RegisterRewardRepository(IMongoSettings settings)
            : base(settings)
        {
        }
    }

 
}
