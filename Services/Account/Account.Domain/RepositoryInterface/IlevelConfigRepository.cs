using Commons.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using Account.Domain.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Account.Domain.RepositoryInterface
{
    public interface ILevelConfigRepository
    {
        List<LevelConfig> LoadLevelConfig();
    }
}
