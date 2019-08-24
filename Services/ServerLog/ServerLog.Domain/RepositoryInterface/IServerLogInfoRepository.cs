using ServerLog.Domain.Models;
using Commons.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Commons.Infrastruct;

namespace ServerLog.Domain.RepositoryInterface
{
    public interface IGameLogInfoRepository
    {
        Task InsertLog(GameLogInfo logInfo);
    }

    public interface IMoneyLogInfoRepository
    {
        Task InsertLog(MoneyLogInfo logInfo);
    }
}
