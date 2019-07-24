using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Domain.Models
{
    /// <summary>
    /// app和server主动发起的请求
    /// </summary>
    public class CommonRequest
    {
        public CommonRequest()
        {

        }

        [JsonConstructor]
        public CommonRequest(string name, string data, Guid id)
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
