﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Extenssions.Defines
{
    /// <summary>
    /// 账号类型
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// Facebook
        /// </summary>
        Facebook = 0,

        /// <summary>
        /// Twitter
        /// </summary>
        Twitter = 1,


    }

    public enum AddReason
    {
        None = 0,
        InitReward = 1,
        RegisterReward = 2,
        BuyIn = 3,
        BackCarry = 4,
        GameAccount = 5,
        LoginReward = 6,
        Bankruptcy = 7,
        Invite = 8,
        GameAct = 9,
    }

    public enum RewardTypes
    {
        Coins = 0,
        Diamonds = 1
    }
}
