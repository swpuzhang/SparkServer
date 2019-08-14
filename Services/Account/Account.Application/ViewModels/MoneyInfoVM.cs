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

        public MoneyInfoVM(long curCoins, long curDiamonds, long maxCoins, long maxDiamonds)
        {
            CurCoins = curCoins;
            CurDiamonds = curDiamonds;
            MaxCoins = maxCoins;
            MaxDiamonds = maxDiamonds;
        }

        public long CurCoins { get; set; }
        public long CurDiamonds { get; set; }
        public long MaxCoins { get; set; }
        public long MaxDiamonds { get; set; }
    }
}
