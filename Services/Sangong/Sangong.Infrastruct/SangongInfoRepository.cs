using Sangong.Domain;
using Sangong.Domain.Models;
using Commons.Infrastruct;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Sangong.Domain.RepositoryInterface;

namespace Sangong.Infrastruct
{
    public class SangongInfoRepository : MongoUserRepository<SangongInfo>, ISangongInfoRepository
    {
        
        public SangongInfoRepository(SangongContext context) : base(context.SangongInfos)
        {

        }
    }
}
