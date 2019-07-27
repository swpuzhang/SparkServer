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
        /// 超时
        /// </summary>
        Timeout =4,
        
        /// <summary>
        /// 客户端已经断开连接
        /// </summary>
        AppIsDisconnected = 5,

        /// <summary>
        /// GUID错误， 可能碰到重复
        /// </summary>
        GuidError = 6,
           
    }
}
