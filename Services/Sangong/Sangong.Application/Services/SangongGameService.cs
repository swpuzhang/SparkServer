using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sangong.Application.ViewModels;
using Sangong.Domain;
using Sangong.Domain.Commands;
using Sangong.Domain.Models;
using Sangong.Domain.RepositoryInterface;
using AutoMapper;
using Commons.Domain.Bus;
using Commons.Domain.Models;
using Commons.Infrastruct;
using Sangong.MqCommands;
using Sangong.Domain.Manager;
using Sangong.MqEvents;
using Newtonsoft.Json;

namespace Sangong.Application.Services
{
    public class SangongGameService : ISangongGameService
    {
        private readonly IMapper _mapper;
        private readonly GameRoomManager _gameRoomManager;
  
        public SangongGameService(IMapper mapper,
            GameRoomManager gameRoomManager)
        {
            _mapper = mapper;
            _gameRoomManager = gameRoomManager;
        }

        public Task<BodyResponse<NullBody>> CreatRoom(CreateRoomMqCommand creatInfo)
        {
            return Task.FromResult(_gameRoomManager.CreateRoom(creatInfo.RoomId, creatInfo.Blind, creatInfo.SeatCount,
                creatInfo.MaxCoins, creatInfo.MaxCoins, creatInfo.TipsPersent, creatInfo.MinCarry, creatInfo.MaxCarry)); 
        }

        public Task<BodyResponse<JoinGameRoomMqResponse>> JoinRoom(JoinGameRoomMqCommand joinInfo)
        {
            return _gameRoomManager.JoinRoom(joinInfo.Id, joinInfo.RoomId);
        }

        public Task<ToAppResponse> GameRoomMessage(RoomRequest request)
        {

            Type t = Type.GetType($"Sangong.GameMessage.{request.ReqName}");
            var obj = JsonConvert.DeserializeObject(request.ReqData, t);
            return Task.FromResult(_gameRoomManager.OnRoomRequest(request.Id, request.RoomId, request.ReqName, obj));
        }

        public void MatchingStarted(string MatchingGroup)
        {
            _gameRoomManager.MatchingStarted(MatchingGroup);
        }
    }
}
