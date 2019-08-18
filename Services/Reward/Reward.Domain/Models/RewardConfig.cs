using System;
using System.Collections.Generic;
using System.Text;

namespace Reward.Domain.Models
{
    public class RegisterRewardConfig
    {
        public static string className = "RegisterRewardConfig";
        public List<long>  DayRewards { get; set; }


    }

    
    public class LoginRewardConfig
    {
        public static string className = "LoginRewardConfig";
        public List<long> DayRewards { get; set; }
    }

    public class BankruptcyConfig
    {
        public static string className = "BankruptcyRewardConfig";
        public List<long> BankruptcyRewards { get; set; }
    }

    public class InviteRewardConfig
    {
        public static string className = "InviteRewardConfig";
        public long InviteRewards { get; set; }
    }

    public class DdvertRewardConfig
    {

    }
}
