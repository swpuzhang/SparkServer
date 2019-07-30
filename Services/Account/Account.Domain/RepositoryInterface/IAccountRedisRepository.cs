using Account.Domain.Models;
using Commons.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.RepositoryInterface
{
    public interface IAccountRedisRepository : IRedisRepository
    {

        Task<LoginCheckInfo> GetLoginCheckInfo(string platformAccount);

        Task SetLoginCheckInfo(string account, AccountInfo info);

        Task SetAccountInfo(AccountInfo info);

        Task<AccountInfo> GetAccountInfo(long id);

        Task<LevelInfo> GetLevelInfo(long id);

        Task<GameInfo> GetGameInfo(long id);

        Task SetLevelInfo(LevelInfo info);

        Task SetGameInfo(GameInfo info);
    }
}
