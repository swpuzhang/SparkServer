using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Domain.Models
{

    public class MoneyInfo
    {
        public MoneyInfo()
        {
        }

        [JsonConstructor]
        public MoneyInfo(long curChips, long curDiamonds, long maxChips, long maxDiamonds)
        {
            CurChips = curChips;
            CurDiamonds = curDiamonds;
            MaxChips = maxChips;
            MaxDiamonds = maxDiamonds;
        }

        public long CurChips { get; private set; }
        public long CurDiamonds { get; private set; }
        public long MaxChips { get; private set; }
        public long MaxDiamonds { get; private set; }
    }
}
