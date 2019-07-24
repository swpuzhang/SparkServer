using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Commons.Extenssions.Defines;

namespace Account.Application.ViewModels
{
    /// <summary>
    /// 接口字段
    /// </summary>
    public class AccountVM
    {

        public Int64 Id { get; set; }
        public string PlatformAccount { get; set; }
        public string UserName { get; set; }
        public int Sex { get; set; }
        public string HeadUrl { get; set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        public AccountType type { get; set; }
        
    }
}
