using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.MqCommands
{
    public class GameInfoMq
    {

        public GameInfoMq()
        {
        }


        public GameInfoMq(int gameTimes, int winTimes, long maxWinCoins)
        {
            GameTimes = gameTimes;
            WinTimes = winTimes;
            MaxWinCoins = maxWinCoins;
        }

        public int GameTimes { get; set; }
        public int WinTimes { get; set; }
        public long MaxWinCoins { get; set; }
    }

    public class LevelInfoMq
    {

        public LevelInfoMq()
        {
        }

        public LevelInfoMq(int curLevel, int curExp, int needExp)
        {
            CurLevel = curLevel;
            CurExp = curExp;
            NeedExp = needExp;
        }

        public int CurLevel { get; set; }

        public int CurExp { get; set; }

        public int NeedExp { get; set; }
    }

    public class GetAccountInfoMqCommand
    {
        public GetAccountInfoMqCommand(long id)
        {
            Id = id;
        }
        public long Id { get; private set; }
    }
    public class GetAccountInfoMqResponse
    {
        public GetAccountInfoMqResponse(long id, string platformAccount,
            string userName, int sex, string headUrl, 
            GameInfoMq gameInfo, LevelInfoMq levelInfo)
        {
            Id = id;
            PlatformAccount = platformAccount;
            UserName = userName;
            Sex = sex;
            HeadUrl = headUrl;
            GameInfo = gameInfo;
            LevelInfo = levelInfo;
        }

        public Int64 Id { get; private set; }
        public string PlatformAccount { get; private set; }
        public string UserName { get; private set; }
        public int Sex { get; set; }
        public string HeadUrl { get; private set; }

        public GameInfoMq GameInfo { get; private set; }

        public LevelInfoMq LevelInfo { get; private set; }

    }
}
