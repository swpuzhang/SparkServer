using Reward.Domain.Models;
using Commons.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reward.Domain.RepositoryInterface
{
    public interface IGameActivityConfigRepository : IConfigListRepository<GameActivityConfig>
    {
    }
}
