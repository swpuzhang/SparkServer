using Account.Domain.Models;
using Account.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Manager
{
    public class LevelManager
    {
        private Dictionary<int, LevelConfig> _levelConfigs = new Dictionary<int, LevelConfig>();
        
        public void LoadConfig(IlevelConfigRepository repository)
        {
            var configs = repository.LoadLevelConfig();
            foreach (var oneConfig in configs)
            {
                _levelConfigs.Add(oneConfig.Level, oneConfig);
            }
        }

        public int GetNeedExp(int curLevel)
        {
            var lastConfig = _levelConfigs[_levelConfigs.Keys.Last<int>()];
            if (curLevel >= lastConfig.NeedExp)
            {
                return lastConfig.NeedExp;
            }
            
            return _levelConfigs[curLevel + 1].NeedExp;
        }

        public bool AddExp(LevelInfo info, int addExp)
        {
            if (!_levelConfigs.TryGetValue(info.CurLevel + 1, out var config))
            {
                return false;
            }

            var lastConfig = _levelConfigs[_levelConfigs.Keys.Last<int>()];
            if (info.CurLevel > lastConfig.Level)
            {
                return false;
            }
            if (info.CurLevel == lastConfig.Level && info.CurExp == lastConfig.NeedExp)
            {
                return false;
            }
            info.CurExp += addExp;
            info.NeedExp = config.NeedExp;
            var remaindExp = info.CurExp - config.NeedExp;
            if (remaindExp < 0)
            {
                //如果是当前经验减成了负数
                if (info.CurExp < 0)
                {
                    info.CurExp = 0;
                }
                return true;
            }
            ++info.CurLevel;
            if (info.CurLevel > lastConfig.Level)
            {
                --info.CurLevel;
                info.CurExp = lastConfig.NeedExp;
                info.NeedExp = lastConfig.NeedExp;
                return true;
            }

            info.CurExp = remaindExp;
            info.NeedExp = config.NeedExp;
            return true;
        }
    }
}
