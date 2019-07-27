using Money.Domain.Models;
using Commons.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Money.Domain.RepositoryInterface
{
    public interface IMoneyInfoRepository : IMongoUserRepository<MoneyInfo>
    {
        MoneyInfo GetByPlatform(string platform);
        Task<MoneyInfo> GetByPlatformAsync(string platform);
    }
}
