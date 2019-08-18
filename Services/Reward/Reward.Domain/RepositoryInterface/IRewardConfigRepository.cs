using Reward.Domain.Models;
using Commons.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reward.Domain.RepositoryInterface
{
    public interface IRegisterRewardConfigRepository
    {
        RegisterRewardConfig LoadConfig();
    }

    public interface ILoginRewardConfigRepository
    {
        LoginRewardConfig LoadConfig();
    }

    public interface IBankruptcyConfigRepository
    {
        BankruptcyConfig LoadConfig();
    }
    public interface IInviteRewardConfigRepository
    {
        InviteRewardConfig LoadConfig();
    }
    
}
