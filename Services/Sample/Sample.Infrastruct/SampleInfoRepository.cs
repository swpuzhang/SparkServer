using Sample.Domain;
using Sample.Domain.Models;
using Commons.Infrastruct;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Sample.Domain.RepositoryInterface;

namespace Sample.Infrastruct
{
    public class SampleInfoRepository : MongoUserRepository<SampleInfo>, ISampleInfoRepository
    {
        
        public SampleInfoRepository(SampleContext context) : base(context.SampleInfos)
        {

        }
    }
}
