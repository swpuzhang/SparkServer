using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Extenssions.Defines
{
    public enum StatuCodeDefines
    {
        Success = 0,
        /// <summary>
        /// 登录失败
        /// </summary>
        LoginError = 1,
        /// <summary>
        /// 字段错误
        /// </summary>
        FieldError = 2,
        /// <summary>
        /// 总线请求错误
        /// </summary>
        BusError = 3,
        /// <summary>
        /// 总线超时
        /// </summary>
        BusTimeout =4
    }
}
