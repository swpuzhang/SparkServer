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
    }
}
