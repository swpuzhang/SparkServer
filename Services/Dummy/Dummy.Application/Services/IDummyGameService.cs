using Dummy.Application.ViewModels;
using Dummy.Domain.Models;
using Commons.Domain.Models;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dummy.MqCommands;
using Dummy.MqEvents;

namespace Dummy.Application.Services
{
    public interface IDummyGameService
    {
        Task<BodyResponse<NullBody>> CreatRoom(CreateRoomMqCommand creatInfo);
        Task<BodyResponse<JoinGameRoomMqResponse>> JoinRoom(JoinGameRoomMqCommand joinInfo);

        Task<ToAppResponse> GameRoomMessage(AppRoomRequest request);

        void MatchingStarted(string MatchingGroup);
        
    }
}
