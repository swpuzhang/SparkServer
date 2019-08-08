using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.ViewModels
{
    public class MoneyInfoVM
    {
        public MoneyInfoVM()
        {
        }

        public MoneyInfoVM(long curCoins, long curDiamonds, long maxChips, long maxDiamonds)
        {
            CurCoins = curCoins;
            CurDiamonds = curDiamonds;
            MaxChips = maxChips;
            MaxDiamonds = maxDiamonds;
        }

        public long CurCoins { get; set; }
        public long CurDiamonds { get; set; }
        public long MaxChips { get; set; }
        public long MaxDiamonds { get; set; }
    }
}
