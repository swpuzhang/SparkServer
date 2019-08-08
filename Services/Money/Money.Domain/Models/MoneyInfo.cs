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
        public MoneyInfo(long id, long curCoins, long curDiamonds, long maxChips, long maxDiamonds, long carry)
        {
            Id = id;
            CurCoins = curCoins;
            CurDiamonds = curDiamonds;
            MaxChips = maxChips;
            MaxDiamonds = maxDiamonds;
            Carry = carry;
        }

        public long CurCoins { get; set; }
        public long CurDiamonds { get; set; }
        public long MaxChips { get; set; }
        public long MaxDiamonds { get; set; }
        public long Carry { get; set; }
    }
}
