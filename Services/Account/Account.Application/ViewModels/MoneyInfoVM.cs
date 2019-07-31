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

        public MoneyInfoVM(long curChips, long curDiamonds, long maxChips, long maxDiamonds)
        {
            CurChips = curChips;
            CurDiamonds = curDiamonds;
            MaxChips = maxChips;
            MaxDiamonds = maxDiamonds;
        }

        public long CurChips { get; set; }
        public long CurDiamonds { get; set; }
        public long MaxChips { get; set; }
        public long MaxDiamonds { get; set; }
    }
}
