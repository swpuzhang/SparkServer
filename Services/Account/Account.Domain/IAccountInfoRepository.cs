using Account.Domain.Models;
using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain
{
    public interface IAccountInfoRepository : IMongoUserRepository<AccountInfo>
    {
        AccountInfo GetByPlatform(string platform);
       
    }
}
