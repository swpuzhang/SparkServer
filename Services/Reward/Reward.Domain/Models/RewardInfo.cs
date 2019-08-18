using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Reward.Domain.Models
{
    public class RegisterRewardInfo : UserEntity
    {
        public static string className = "RegisterRewardInfo";
        public int DayIndex { get; private set; }
        public DateTime GetDate { get; private set; }
        [JsonConstructor]
        public RegisterRewardInfo(long id, int dayIndex, DateTime getDate)
        {
            Id = id;
            DayIndex = dayIndex;
            GetDate = getDate;
        }
    }


    /// <summary>
    /// 注册奖励
    /// </summary>
    public class RegisterRewardVM
    {
        public enum RewardState
        {
            None = 0,
            NotBegin = 1,
            Available = 2,
            Getted = 3,
            Over = 4,
        }

        public RegisterRewardVM(RewardState state, int dayIndex, List<long> rewardConfig)
        {
            State = state;
            DayIndex = dayIndex;
            RewardConfig = rewardConfig;
        }

        /// <summary>
        /// 状态
        ///  None = 0, 没有注册奖励
        /// NotBegin = 1:还未开始
        /// Available = 2,可以领取
        /// Getted = 3,已经被领取
        /// Over = 4,奖励已经结束
        /// </summary>
        public RewardState State { get; set; }
        /// <summary>
        /// 领取的第几天
        /// </summary>
        public int DayIndex { get; set; }
        /// <summary>
        /// 奖励配置, 每一天奖励的金币数量
        /// </summary>
        public List<long> RewardConfig { get; set; }
    }

    /// <summary>
    /// 领取奖励响应
    /// </summary>
    public class RewardInfoVM
    {
        public RewardInfoVM(long rewardCoins)
        {
            RewardCoins = rewardCoins;
        }

        /// <summary>
        /// 领取的金币奖励
        /// </summary>
        public long RewardCoins { get; set; }
    }

    public class LoginRewardInfo : UserEntity
    {
        public static string className = "LoginReward";
        public LoginRewardInfo(long id,  List<int> gettedDays)
            
        {
            Id = id;
            GettedDays = gettedDays;
        }

        public List<int> GettedDays { get; private set; }
    }


    public class LoginRewardVM
    {
        public static string className = "LoginReward";
        public class OneReward
        {
            public int DayIndex { get; set; }
            public OneRewardState state {get; set;}
            public long RewardCoins { get; set; }
        }
        public enum OneRewardState
        {

            Getted = 1,
            NotGetted = 2,
            Available = 3,
            Waitting = 4,
        }

        public LoginRewardVM(List<OneReward> rewardConfig)
        {
            RewardConfig = rewardConfig;
        }

        /// <summary>
        /// 奖励配置, 每一天奖励的金币数量
        /// </summary>
        public List<OneReward> RewardConfig { get; set; }
    }

    public class BankruptcyInfo : UserEntity
    {

        public static string className = "BankruptcyInfo";

        public BankruptcyInfo(int curTimes)
        {
            CurTimes = curTimes;
        }

        public int CurTimes { get; set; }
    }

    public class BankruptcyInfoVM
    {
       
        public enum BankruptcyRewardType
        {
            //每天破产补助
            Day,
            //终身破产补助
            lifelong
        }

        public BankruptcyInfoVM(BankruptcyRewardType type, int totalTimes, 
            int curTimes, List<long> rewardConfig)
        {
            Type = type;
            TotalTimes = totalTimes;
            CurTimes = curTimes;
            RewardConfig = rewardConfig;
        }



        /// <summary>
        ///每天破产补助
        ///Day,
        ///终身破产补助
        ///lifelong
        /// </summary>
        public BankruptcyRewardType Type { get; set; }
        /// <summary>
        /// 总共几次破产机会
        /// </summary>
        public int TotalTimes { get; set; }
        /// <summary>
        /// 当前是第几次
        /// </summary>
        public int CurTimes { get; set; }
        /// <summary>
        /// 破产几次的配置
        /// </summary>
        /// 
        public List<long> RewardConfig { get; set; }
    }

    public class InviteRewardInfo : UserEntity
    {

        public static string className = "InviteRewardInfo";

        public InviteRewardInfo(int curTimes)
        {
            CurTimes = curTimes;
        }

        public int CurTimes { get; set; }
    }
}
