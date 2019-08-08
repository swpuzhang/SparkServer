using Sangong.Application.ViewModels;
using Sangong.Domain.Models;
using Commons.Domain.Models;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sangong.MqCommands;
using Sangong.MqEvents;

namespace Sangong.Application.Services
{
    public interface ISangongGameService
    {
        Task<BaseResponse> CreatRoom(CreateRoomMqCommand creatInfo);
        Task<BodyResponse<JoinGameRoomMqResponse>> JoinRoom(JoinGameRoomMqCommand joinInfo);

        Task<CommonResponse> GameRoomMessage(RoomRequest request);

        void MatchingStarted(string MatchingGroup);
        
    }
}
