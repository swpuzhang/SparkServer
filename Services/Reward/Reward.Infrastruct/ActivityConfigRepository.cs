using Commons.Infrastruct;
using Reward.Domain.Models;
using Reward.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reward.Infrastruct
{
    public class GameActivityConfigRepository : 
        ConfigListRepository<GameActivityConfig>,
        IGameActivityConfigRepository
    {
        public GameActivityConfigRepository(IMongoSettings settings)
            :base(settings)
        {

        }
    }
}
