using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Commons.Extenssions.Defines;

namespace Sangong.Application.ViewModels
{
    /// <summary>
    /// 匹配结果
    /// </summary>
    public class MatchingResponseVM
    {
        public string RoomId { get; set; }
        public long Blind { get; set; }
        /// <summary>
        /// 请求gameserver的Key
        /// </summary>
        public string GameKey { get; set; }
    }

   

        
}
