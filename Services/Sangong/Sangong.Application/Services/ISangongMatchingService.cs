﻿using Sangong.Application.ViewModels;
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
    public interface ISangongMatchingService
    {
        Task<BodyResponse<MatchingResponseVM>> Playnow(long id);
        Task<BodyResponse<GetBlindRoomListResponse>> GetBlindRoomList(long id);
        Task<BodyResponse<MatchingResponseVM>> BlindMatching(long id, long blind);
        
        void SynGameRooms(SyncGameRoomMqCommand command);
        void OnJoinGameRoom(JoinGameRoomMqEvent joinEvent);
        void OnLeaveGameRoom(LeaveGameRoomMqEvent leaveEvent);

        Task<BodyResponse<NullBody>> OnUserApplySit(UserApplySitMqCommand sitcmd);
        void OnUserSiteFailed(UserSitFailedMqEvent sitEvent);
    }
}
