using Sample.Domain.Models;
using Commons.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.RepositoryInterface
{
    public interface IUserIdGenRepository : IMongoUserRepository<UserIdGenInfo>
    {
        Task<Int64> GenNewId();
    }
}
