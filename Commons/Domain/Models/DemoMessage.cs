using Commons.Extenssions.Defines;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Domain.Models
{

    /// <summary>
    /// Server推送APP的消息
    /// </summary>
    public class ToAppRequest<T>
        where T : class
    {
        /// <summary>
        /// 消息的唯一码
        /// </summary>
        public Guid MessageId { get; set; }

        /// <summary>
        /// 请求的消息名
        /// </summary>
        public string ReqName { get; set; }
        /// <summary>
        /// 请求的消息数据
        /// </summary>
        public T ReqData { get; set; }
    }

    /// <summary>
    /// server推送房间消息
    /// </summary>
    public class ToAppRoomRequest<T>
         where T : class
    {
       
        /// <summary>
        /// 消息的唯一码
        /// </summary>
        public Guid MessageId { get; set; }

        /// <summary>
        /// gamekey 标识哪个gamesvr
        /// </summary>
        public string GameKey { get; set; }

        /// <summary>
        /// 标识那个房间
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// 请求的消息名
        /// </summary>
        public string ReqName { get; set; }
        /// <summary>
        /// 请求的消息数据
        /// </summary>
        public T ReqData { get; set; }
    }

    /// <summary>
    /// App发送给server的房间消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AppRoomRequest<T>
        where T : class
    {
        /// <summary>
        /// 玩家ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 请求的消息名, ReqData的消息名
        /// </summary>
        public string ReqName { get; set; }
        /// <summary>
        /// 请求的消息数据
        /// </summary>
        public T ReqData { get; set; }
        /// <summary>
        /// 房间key, 这个key通过匹配接口返回
        /// </summary>
        public string GameRoomKey { get; set; }
        /// <summary>
        /// 房间ID 通过匹配返回
        /// </summary>
        public string RoomId { get; set; }

    }

    /// <summary>
    /// 给APP返回的response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ToAppResponse<T>
        where T : class
    {

        /// <summary>
        /// 消息数据，如果没有就为Null
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 返回码
        /// </summary>
        public StatusCodeDefines StatusCode { get; set; }

        /// <summary>
        /// 错误信息 如果没有就为Null
        /// </summary>
        public List<string> ErrorInfos { get; set; }
    }

    /// <summary>
    /// APP给server返回的response
    /// </summary>
    public class ToServerResponse<T>
        where T : class
    {
        /// <summary>
        /// 返回请求消息的唯一码
        /// </summary>
        public Guid MessageId { get; set; }

        /// <summary>
        /// 消息数据
        /// </summary>
        public T Data { get; set; }

        public StatusCodeDefines StatusCode { get; set; }

        public List<string> ErrorInfos { get; set; }
    }



}
