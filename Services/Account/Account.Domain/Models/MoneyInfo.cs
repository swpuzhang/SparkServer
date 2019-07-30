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
