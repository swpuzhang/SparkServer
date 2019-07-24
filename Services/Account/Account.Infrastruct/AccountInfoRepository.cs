using Account.Domain;
using Account.Domain.Models;
using Commons.Infrastruct;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Account.Domain.RepositoryInterface;

namespace Account.Infrastruct
{
    public class AccountInfoRepository : MongoUserRepository<AccountInfo>, IAccountInfoRepository
    {
        
        public AccountInfoRepository(AccountContext context) : base(context.AccountInfos)
        {

        }

        public AccountInfo GetByPlatform(string platform)
        {
            return _dbCol.Find<AccountInfo>(e => e.PlatformAccount == platform).FirstOrDefault();
        }

        public async Task<AccountInfo> GetByPlatformAsync(string platform)
        {
            var all = await _dbCol.FindAsync<AccountInfo>(e => e.PlatformAccount == platform);
            if (all == null)
            {
                return null;
            }
            if (!await all.MoveNextAsync())
            {
                return null;
            }
            return await all.FirstOrDefaultAsync();
        }
    }
}
