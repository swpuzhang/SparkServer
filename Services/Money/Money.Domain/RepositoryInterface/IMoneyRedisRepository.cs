using Commons.Domain.RepositoryInterface;
using Money.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Money.Domain.RepositoryInterface
{
    public interface IMoneyRedisRepository : IRedisRepository
    {
        Task<MoneyInfo> GetMoney(long id);
        Task SetMoney(long id, MoneyInfo info);
    }
}
