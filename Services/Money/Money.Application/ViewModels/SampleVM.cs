using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Commons.Extenssions.Defines;

namespace Sample.Application.ViewModels
{
    /// <summary>
    /// 接口字段
    /// </summary>
    public class SampleVM
    {
        public Int64 Id { get; set; }
        public Int64 Coins { get; set; }
    }
}
