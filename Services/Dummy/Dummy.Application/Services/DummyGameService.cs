using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dummy.Application.ViewModels;
using Dummy.Domain;
using Dummy.Domain.Commands;
using Dummy.Domain.Models;
using Dummy.Domain.RepositoryInterface;
using AutoMapper;
using Commons.Domain.Bus;
using Commons.Domain.Models;
using Commons.Infrastruct;
using Dummy.MqCommands;
using Dummy.Domain.Manager;
using Dummy.MqEvents;
using Newtonsoft.Json;

namespace Dummy.Application.Services
{
    public class DummyGameService : IDummyGameService
    {
        private readonly IMapper _mapper;
        private readonly GameRoomManager _gameRoomManager;
  
        public DummyGameService(IMapper mapper,
            GameRoomManager gameRoomManager)
        {
            _mapper = mapper;
            _gameRoomManager = gameRoomManager;
        }

        public Task<BodyResponse<NullBody>> CreatRoom(CreateRoomMqCommand creatInfo)
        {
            return Task.FromResult(_gameRoomManager.CreateRoom(creatInfo.RoomType, creatInfo.RoomId, creatInfo.Blind, creatInfo.SeatCount,
                creatInfo.MaxCoins, creatInfo.MaxCoins, creatInfo.TipsPersent, creatInfo.MinCarry, creatInfo.MaxCarry)); 
        }

        public Task<BodyResponse<JoinGameRoomMqResponse>> JoinRoom(JoinGameRoomMqCommand joinInfo)
        {
            return _gameRoomManager.JoinRoom(joinInfo.Id, joinInfo.RoomId);
        }

        public Task<ToAppResponse> GameRoomMessage(AppRoomRequest request)
        {

            Type t = Type.GetType($"Dummy.GameMessage.{request.ReqName}");
            var obj = JsonConvert.DeserializeObject(request.ReqData, t);
            return Task.FromResult(_gameRoomManager.OnRoomRequest(request.Id, request.RoomId, request.ReqName, obj));
        }

        public void MatchingStarted(string MatchingGroup)
        {
            _gameRoomManager.MatchingStarted(MatchingGroup);
        }
    }
}
