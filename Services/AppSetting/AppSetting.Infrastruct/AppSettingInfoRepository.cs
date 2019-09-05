using AppSetting.Domain;
using AppSetting.Domain.Models;
using Commons.Infrastruct;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using AppSetting.Domain.RepositoryInterface;

namespace AppSetting.Infrastruct
{
    public class FadebackInfoRepository : MongoOneContext<FadebackInfo>, IFadebackInfoRepository
    {
        public FadebackInfoRepository(IMongoSettings settings) : base(settings)
        {

        }
    }
}
