using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Commons.Extenssions.Defines;

namespace Money.Application.ViewModels
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

        public long CurCoins { get; private set; }
        public long CurDiamonds { get; private set; }
        public long MaxCoins { get; private set; }
        public long MaxDiamonds { get; private set; }
    }
}
