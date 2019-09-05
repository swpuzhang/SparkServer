using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameSample.Application.ViewModels;
using GameSample.Domain;
using GameSample.Domain.Commands;
using GameSample.Domain.Models;
using GameSample.Domain.RepositoryInterface;
using AutoMapper;
using Commons.Domain.Bus;
using Commons.Domain.Models;
using Commons.Infrastruct;
using GameSample.MqCommands;
using GameSample.Domain.Manager;
using GameSample.MqEvents;
using Newtonsoft.Json;

namespace GameSample.Application.Services
{
    public class GameSampleGameService : IGameSampleGameService
    {
        private readonly IMapper _mapper;
        private readonly GameRoomManager _gameRoomManager;
  
        public GameSampleGameService(IMapper mapper,
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

            Type t = Type.GetType($"GameSample.GameMessage.{request.ReqName}");
            var obj = JsonConvert.DeserializeObject(request.ReqData, t);
            return Task.FromResult(_gameRoomManager.OnRoomRequest(request.Id, request.RoomId, request.ReqName, obj));
        }

        public void MatchingStarted(string MatchingGroup)
        {
            _gameRoomManager.MatchingStarted(MatchingGroup);
        }
    }
}
