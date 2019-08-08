using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Extenssions.Defines
{
    public enum StatuCodeDefines
    {
        /// <summary>
        /// 通用错误
        /// </summary>
        Error = -1,

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
        /// <summary>
        /// 验证token失败
        /// </summary>
        TokenError = 7,

        /// <summary>
        /// token过期, 重新登录
        /// </summary>
        TokenExpiredPleaseRelogin =8,

        /// <summary>
        /// huoq
        /// </summary>
        GetMoneyError = 9,

        AccountError = 10,


        GameInfoError = 11,

        IsMatching = 12,

        /// <summary>
        /// 钱不够
        /// </summary>
        NoEnoughMoney = 13,

        PlayerNotInRoom = 14,

      

    }
}
