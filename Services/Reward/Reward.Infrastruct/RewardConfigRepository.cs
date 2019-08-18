using Reward.Domain;
using Reward.Domain.Models;
using Commons.Infrastruct;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Reward.Domain.RepositoryInterface;

namespace Reward.Infrastruct
{
    public class RegisterRewardConfigRepository : MongoOneContext<RegisterRewardConfig>, IRegisterRewardConfigRepository
    {

        public RegisterRewardConfigRepository(IMongoSettings settings)
            : base(settings)
        {
        }


        public RegisterRewardConfig LoadConfig()
        {

            var configs = _dbCol.Find<RegisterRewardConfig>(x => true);//.Project(Builders<LevelConfig>.
                                                                 //Projection.Exclude("_id")).ToList();

            return configs.Project<RegisterRewardConfig>(Builders<RegisterRewardConfig>.Projection.Exclude("_id")).ToList().First();
        }
    }

    public class LoginRewardConfigRepository : MongoOneContext<LoginRewardConfig>, ILoginRewardConfigRepository
    {

        public LoginRewardConfigRepository(IMongoSettings settings)
            : base(settings)
        {
        }

        public LoginRewardConfig LoadConfig()
        {
            var configs = _dbCol.Find<LoginRewardConfig>(x => true);//.Project(Builders<LevelConfig>.
                                                                       //Projection.Exclude("_id")).ToList();

            return configs.Project<LoginRewardConfig>(Builders<LoginRewardConfig>.Projection.Exclude("_id")).ToList().First();
        }

    }

    public class BankruptcyConfigRepository : MongoOneContext<BankruptcyConfig>, IBankruptcyConfigRepository
    {

        public BankruptcyConfigRepository(IMongoSettings settings)
            : base(settings)
        {
        }

        public BankruptcyConfig LoadConfig()
        {
            var configs = _dbCol.Find<BankruptcyConfig>(x => true);//.Project(Builders<LevelConfig>.
                                                                    //Projection.Exclude("_id")).ToList();

            return configs.Project<BankruptcyConfig>(Builders<BankruptcyConfig>.Projection.Exclude("_id")).ToList().First();
        }

    }

    public class InviteRewardConfigRepository : MongoOneContext<InviteRewardConfig>, IInviteRewardConfigRepository
    {

        public InviteRewardConfigRepository(IMongoSettings settings)
            : base(settings)
        {
        }

        public InviteRewardConfig LoadConfig()
        {
            var configs = _dbCol.Find(x => true);//.Project(Builders<LevelConfig>.
                                                                   //Projection.Exclude("_id")).ToList();

            return configs.Project<InviteRewardConfig>(Builders<InviteRewardConfig>.Projection.Exclude("_id")).ToList().First();
        }

    }

}
