﻿using Commons.Extenssions.Defines;
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
        public ToAppRequest(string name, string data, Guid gid)
        {
            ReqName = name;
            ReqData = data;
            MessageId = gid;
        }

        /// <summary>
        /// 消息的唯一码
        /// </summary>
        public Guid MessageId { get; private set; }

        /// <summary>
        /// 请求的消息名
        /// </summary>
        public string ReqName { get; private set; }
        /// <summary>
        /// 请求的消息数据
        /// </summary>
        public string ReqData { get; private set; }
    }


    public class ToAppRoomRequest
    {
        public ToAppRoomRequest()
        {

        }

        public static string RequestName = "ToAppRoomRequest";

        [JsonConstructor]
        public ToAppRoomRequest(string name, string data, Guid gid, string gameKey, int roomId)
        {
            ReqName = name;
            ReqData = data;
            MessageId = gid;
            GameKey = gameKey;
            RoomId = roomId;
        }

        /// <summary>
        /// 消息的唯一码
        /// </summary>
        public Guid MessageId { get; private set; }

        /// <summary>
        /// gamekey 标识哪个gamesvr
        /// </summary>
        public string GameKey { get; private set; }

        /// <summary>
        /// 标识那个房间
        /// </summary>
        public int RoomId { get; private set; }

        /// <summary>
        /// 请求的消息名
        /// </summary>
        public string ReqName { get; private set; }
        /// <summary>
        /// 请求的消息数据
        /// </summary>
        public string ReqData { get; private set; }
    }

    public class ServerRequest1<T> where T : class
    {
        public ServerRequest1()
        {
        }

        public ServerRequest1(T body)
        {
            this.body = body;
        }

        public T body {get; private set;}
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
        public ServerRequest(int id, string name, string data, Guid gid)
        {
            Id = id;
            ReqName = name;
            ReqData = data;
            MessageId = gid;
        }

        public int Id { get; set; }

        /// <summary>
        /// 消息的唯一码
        /// </summary>
        public Guid MessageId { get; private set; }

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
    /// GameServer内部请求
    /// </summary>
    public class GameServerRequest
    {
        public GameServerRequest()
        {

        }

        [JsonConstructor]
        public GameServerRequest(long userId, object request, string reqName, string gameKey, string roomId)
        {
            Id = userId;
            ReqName = reqName;
            ReqData = JsonConvert.SerializeObject(request);
            MessageId = Guid.NewGuid();
            GameKey = gameKey;
            RoomId = roomId;
        }

        public long Id { get; set; }

        /// <summary>
        /// 消息的唯一码
        /// </summary>
        public Guid MessageId { get; private set; }

        /// <summary>
        /// 请求的消息名
        /// </summary>
        public string ReqName { get; private set; }
        /// <summary>
        /// 请求的消息数据
        /// </summary>
        public string ReqData { get; private set; }

        /// <summary>
        /// gamekey 标识哪个gamesvr
        /// </summary>
        public string GameKey { get; private set; }

        /// <summary>
        /// 标识那个房间
        /// </summary>
        public string RoomId { get; private set; }
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
        public AppRequest(long id, string name, string data, Guid messageId)
        {
            ReqName = name ?? "";
            ReqData = data ?? "";
            MessageId = messageId;
            Id = id;
        }

        public long Id { get; private set; }

        /// <summary>
        /// 消息的唯一码
        /// </summary>
        public Guid MessageId { get; private set; }

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
        public RoomRequest(long id, string name, string data, Guid gid, string gameRoomKey, string roomId) :
           base(id, name, data, gid)
        {
            GameRoomKey = gameRoomKey;
            RoomId = roomId;
        }

        public string GameRoomKey { get; private set; }
        public string RoomId { get; private set; }
    }

    public class ToAppResponse
    {

        [JsonConstructor]
        public ToAppResponse(object data, StatusCodeDefines statusCode, List<string> errorInfos)
        {
            if (data == null)
            {
                Data = null;
            }
            else
            {
                Data = JsonConvert.SerializeObject(data);
            }
            StatusCode = statusCode;
            ErrorInfos = errorInfos;
        }

        public ToAppResponse()
        {
            Data = null;
            StatusCode = StatusCodeDefines.Success;
            ErrorInfos = null;
        }

        /// <summary>
        /// 消息数据
        /// </summary>
        public string Data { get; private set; }

        public StatusCodeDefines StatusCode { get; private set; }

        public List<string> ErrorInfos { get; private set; }
    }

    /// <summary>
    /// response
    /// </summary>
    public class ToServerResponse
    {
        public ToServerResponse()
        {

        }

        [JsonConstructor]
        public ToServerResponse(object data, Guid gid, StatusCodeDefines statusCode, List<string> errorInfos)
        {
            if (data == null)
            {
                Data = null;
            }
            else
            {
                Data = JsonConvert.SerializeObject(data);
            }
            
            MessageId = gid;
            StatusCode = statusCode;
            ErrorInfos = errorInfos;
        }

        public ToServerResponse(Guid gid)
        {
            Data = null;
            MessageId = gid;
            StatusCode = StatusCodeDefines.Success;
            ErrorInfos = null;
        }

        /// <summary>
        /// 返回请求消息的唯一码
        /// </summary>
        public Guid MessageId { get; private set; }

        /// <summary>
        /// 消息数据
        /// </summary>
        public string Data { get; private set; }

        public StatusCodeDefines StatusCode { get; private set; }
        public List<string> ErrorInfos { get; private set; }
    }

    

}