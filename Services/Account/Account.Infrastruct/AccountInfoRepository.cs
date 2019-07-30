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
using MongoDB.Bson;

namespace Account.Infrastruct
{
    public class AccountInfoRepository : MongoUserRepository<AccountInfo>, IAccountInfoRepository
    {
        
        public AccountInfoRepository(AccountContext context) : base(context.AccountInfos)
        {

        }


        public async Task<AccountInfo> GetByPlatform(string platform)
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

        public Task Update(AccountInfo account)
        {
            _dbCol.UpdateOneAsync<AccountInfo>(e => e.Id == account.Id,
                BsonDocument.Create(account));
            return Task.CompletedTask;
        }
    }
}
