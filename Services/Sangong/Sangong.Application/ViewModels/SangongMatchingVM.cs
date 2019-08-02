﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Commons.Extenssions.Defines;

namespace Sangong.Application.ViewModels
{
    /// <summary>
    /// 接口字段
    /// </summary>
    public class SangongMatchingResponseVM
    {
        public int RoomId {get; set;}
        public long Blind { get; set;}
        public string GameKey { get; set; }
    }

}
