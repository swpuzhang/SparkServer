using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.ViewModels
{
    public class LevelInfoVM
    {
    
        public LevelInfoVM()
        {
        }

        public LevelInfoVM(int curLevel, int curExp, int needExp)
        {
            CurLevel = curLevel;
            CurExp = curExp;
            NeedExp = needExp;
        }

        public int CurLevel { get; set; }

        public int CurExp { get; set; }

        public int NeedExp { get; set; }
    }
}
