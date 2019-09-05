using GameSample.Application.ViewModels;
using GameSample.Domain.Models;
using Commons.Domain.Models;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameSample.MqCommands;
using GameSample.MqEvents;

namespace GameSample.Application.Services
{
    public interface IGameSampleGameService
    {
        Task<BodyResponse<NullBody>> CreatRoom(CreateRoomMqCommand creatInfo);
        Task<BodyResponse<JoinGameRoomMqResponse>> JoinRoom(JoinGameRoomMqCommand joinInfo);

        Task<ToAppResponse> GameRoomMessage(AppRoomRequest request);

        void MatchingStarted(string MatchingGroup);
        
    }
}
