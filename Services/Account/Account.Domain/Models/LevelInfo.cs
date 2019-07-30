using Commons.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Models
{


    public class LevelInfo : UserEntity
    {
        public static string ClassName = "LevelInfo";
        public LevelInfo()
        {
        }

        [JsonConstructor]
        public LevelInfo(long id, int curLevel, int curExp, int needExp)
        {
            Id = id;
            CurLevel = curLevel;
            CurExp = curExp;
            NeedExp = needExp;
        }

        public int CurLevel { get; set; }

        public int CurExp { get; set; }

        public int NeedExp { get; set; }
    }


    public class LevelConfig
    {
        public LevelConfig()
        {
        }

        [JsonConstructor]
        public LevelConfig(int level, int expNeed)
        {
            Level = level;
            NeedExp = expNeed;
        }

        public int Level { get; private set; } 
        public int NeedExp { get; private set; }
    }
}
