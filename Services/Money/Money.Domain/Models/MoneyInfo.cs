using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Money.Domain.Models
{

    public class MoneyInfo : UserEntity
    {
        public static string ClassName = "MoneyInfo";
        public MoneyInfo()
        {
        }

        [JsonConstructor]
        public MoneyInfo(long id, long curCoins, long curDiamonds, long maxCoins, long maxDiamonds, long carry)
        {
            Id = id;
            CurCoins = curCoins;
            CurDiamonds = curDiamonds;
            MaxCoins = maxCoins;
            MaxDiamonds = maxDiamonds;
            Carry = carry;
        }

        public void AddCoins(long coins)
        {
            CurCoins += coins;
        }

        public void AddCarry(long coins)
        {
            Carry += coins;
        }

        public void UpdateMaxCoins()
        {
            if (CurCoins + Carry > MaxCoins)
            {
                MaxCoins = CurCoins + Carry;
            }
        }

        public long CurCoins { get; private set; }
        
        public long CurDiamonds { get; private set; }
        public long MaxCoins { get; private set; }
        public long MaxDiamonds { get; private set; }
        public long Carry { get; private set; }
    }
}
