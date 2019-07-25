using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Domain.Models
{

    /// <summary>
    /// Server推送
    /// </summary>
    public class ToAppRequest
    {
        public ToAppRequest()
        {

        }

        public static string RequestName = "ToAppRequest";

        [JsonConstructor]
        public ToAppRequest(string name, string data, Guid id)
        {
            ReqName = name;
            ReqData = data;
            Id = id;
        }

        /// <summary>
        /// 消息的唯一码
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// 请求的消息名
        /// </summary>
        public string ReqName { get; private set; }
        /// <summary>
        /// 请求的消息数据
        /// </summary>
        public string ReqData { get; private set; }
    }

    /// <summary>
    /// Server内部请求
    /// </summary>
    public class ServerRequest
    {
        public ServerRequest()
        {

        }

        [JsonConstructor]
        public ServerRequest(int userId, string name, string data, Guid id)
        {
            UserId = userId;
            ReqName = name;
            ReqData = data;
            Id = id;
        }

        public int UserId { get; set; }

        /// <summary>
        /// 消息的唯一码
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// 请求的消息名
        /// </summary>
        public string ReqName { get; private set; }
        /// <summary>
        /// 请求的消息数据
        /// </summary>
        public string ReqData { get; private set; }
    }

    /// <summary>
    /// APP主动请求
    /// </summary>
    public class AppRequest
    {
        public AppRequest()
        {

        }

        [JsonConstructor]
        public AppRequest(string name, string data, Guid id)
        {
            ReqName = name ?? "";
            ReqData = data ?? "";
            Id = id;
        }

        /// <summary>
        /// 消息的唯一码
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// 请求的消息名
        /// </summary>
        public string ReqName { get; private set; }
        /// <summary>
        /// 请求的消息数据
        /// </summary>
        public string ReqData { get; private set; }
    }

    /// <summary>
    /// APP请求房间
    /// </summary>
    public class RoomRequest : AppRequest
    {
        public RoomRequest()
        {
        }

        [JsonConstructor]
        public RoomRequest(string name, string data, Guid id, int roomId):
           base(name, data, id)
        {
            RoomId = roomId;
        }

        public int RoomId { get; private set; }
    }

    /// <summary>
    /// response
    /// </summary>
    public class CommonResponse
    {
        public CommonResponse()
        {

        }

        [JsonConstructor]
        public CommonResponse(string data, Guid id)
        {
            Data = data ?? "";
            Id = id;
        }

        /// <summary>
        /// 返回请求消息的唯一码
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// 消息数据
        /// </summary>
        public string Data { get; private set; }

        public static implicit operator CommonResponse(RoomRequest v)
        {
            throw new NotImplementedException();
        }
    }

    public class BaseRoomMessage
    {
        public BaseRoomMessage()
        {
        }

        [JsonConstructor]
        public BaseRoomMessage(int roomId, int userId)
        {
            RoomId = roomId;
            UserId = userId;
        }


        public int RoomId { get; private set; }


        public int UserId { get; private set; }

    }

}
