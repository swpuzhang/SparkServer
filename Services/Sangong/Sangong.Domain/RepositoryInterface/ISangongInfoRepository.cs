using Sangong.Domain.Models;
using Commons.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sangong.Domain.RepositoryInterface
{
    public interface ISangongInfoRepository : IMongoUserRepository<SangongInfo>
    {
       
    }
}
