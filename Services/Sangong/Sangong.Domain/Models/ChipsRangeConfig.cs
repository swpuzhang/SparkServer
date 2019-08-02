using System;
using System.Collections.Generic;
using System.Text;

namespace Sangong.Domain.Models
{
    public class CoinsRangeConfig
    {
        public long CoinsBegin { get; set; }
        /// <summary>
        /// 不包含ChipsEnd
        /// </summary>
        public long CoinsEnd { get; set; } = long.MaxValue;

        public long Blind { get; set; }
    }
}
