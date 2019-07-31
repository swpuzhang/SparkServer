using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.ViewModels
{
    public class GameInfoVM
    {

        public GameInfoVM()
        {
        }


        public GameInfoVM(int gameTimes, int winTimes, long maxWinCoins)
        {
            GameTimes = gameTimes;
            WinTimes = winTimes;
            MaxWinCoins = maxWinCoins;
        }

        public int GameTimes { get; set; }
        public int WinTimes { get; set; }
        public long MaxWinCoins { get; set; }
    }
}
