﻿using Commons.Domain.Models;
using Commons.Extenssions.Defines;
using Microsoft.Extensions.Configuration;
using Sangong.Domain.Models;
using Sangong.MqCommands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Commons.IntegrationBus;
using Commons.Domain.Managers;
using Sangong.Domain.Logic;
using MassTransit;
using AutoMapper;

namespace Sangong.Domain.Manager
{
    public class GameRoomManager
    {
        public static string gameKey = string.Empty;
        public static string matchingGroup = string.Empty;
        private Dictionary<string, GameRoom> _rooms = new Dictionary<string, GameRoom>();
        private readonly MqManager _mqManager;
        private readonly IBusControl _bus;
        private IMapper _mapper;
        public GameRoomManager(IConfiguration configuration, MqManager mqManager, IBusControl bus, IMapper mapper)
        {
            var mqcfg = configuration.GetSection("Rabbitmq");
            gameKey = mqcfg["Queue"];
            matchingGroup = configuration["MatchingGroup"];
            _mqManager = mqManager;
            _bus = bus;
            _mapper = mapper;
        }

        public BaseResponse CreateRoom(string roomId, long blind, int seatCount, 
            long minCoins, long maxCoins, int tipsPersent,
            long minCarry, long maxCarry)
        {
            if (_rooms.ContainsKey(roomId))
            {
                return new BaseResponse(StatuCodeDefines.Error, new List<string>() { "room has already created" });
            }
            GameRoom roomInfo = new GameRoom(roomId, blind, seatCount, minCoins, 
                maxCoins, tipsPersent, _mqManager, _bus, _mapper, minCarry, maxCarry);
            roomInfo.Init();
            _rooms.Add(roomId, roomInfo);
            return new BaseResponse(StatuCodeDefines.Success, null);
        }

        public async Task<BodyResponse<JoinGameRoomMqResponse>> JoinRoom(long id, string roomId)
        {
            if (!_rooms.TryGetValue(roomId, out var oneRoom))
            {
                return new BodyResponse<JoinGameRoomMqResponse>(StatuCodeDefines.Error, new List<string>() { "room is not exist" }, null);
            }
            return await oneRoom.JoinRoom(id);
        }

        public CommonResponse OnRoomRequest(long id, string roomId, Guid gid, string requestName, object request)
        {
            if (!_rooms.TryGetValue(roomId, out var oneRoom))
            {
                return new CommonResponse(null, gid, StatuCodeDefines.Error, null);
            }
            try
            {
                var handler = typeof(GameRoom).GetMethod($"On{requestName}");
                return handler.Invoke(oneRoom, new object[] { id, gid, request }) as CommonResponse;
            }
            catch
            {
                return new CommonResponse(null, gid, StatuCodeDefines.Error, null);
            }
            
        }

        public void MatchingStarted(string group)
        {
            if (matchingGroup != group)
            {
                return;
            }
            List<SyncRoomInfo> rooms = new List<SyncRoomInfo>();
            foreach (var room in _rooms)
            {
                rooms.Add(new SyncRoomInfo(room.Value.RoomId, room.Value.GetPlayerCount(), room.Value.Blind));
            }
            _bus.Publish(new SyncGameRoomMqCommand(gameKey, matchingGroup, rooms));
        }
    }
}