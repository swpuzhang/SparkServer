using Money.Domain;
using Money.Domain.Models;
using Commons.Infrastruct;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Money.Domain.RepositoryInterface;

namespace Money.Infrastruct
{
    public class MoneyInfoRepository : MongoUserRepository<MoneyInfo>, IMoneyInfoRepository
    {
        
        public MoneyInfoRepository(MoneyContext context) : base(context.MoneyInfos)
        {

        }

        public MoneyInfo GetByPlatform(string platform)
        {
            return _dbCol.Find<MoneyInfo>(e => e.PlatformMoney == platform).FirstOrDefault();
        }

        public async Task<MoneyInfo> GetByPlatformAsync(string platform)
        {
            var all = await _dbCol.FindAsync<MoneyInfo>(e => e.PlatformMoney == platform);
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
