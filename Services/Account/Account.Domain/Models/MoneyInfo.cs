using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Models
{
    public class MoneyInfo
    {
        public MoneyInfo()
        {
        }

        [JsonConstructor]
        public MoneyInfo(long curCoins, long curDiamonds, long maxChips, long maxDiamonds)
        {
            CurCoins = curCoins;
            CurDiamonds = curDiamonds;
            MaxChips = maxChips;
            MaxDiamonds = maxDiamonds;
        }

        public long CurCoins { get; private set; }
        public long CurDiamonds { get; private set; }
        public long MaxChips { get; private set; }
        public long MaxDiamonds { get; private set; }
    }
}
