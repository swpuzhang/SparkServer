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

namespace Sangong.Application.Services
{
    public class SangongMatchingService : ISangongMatchingService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly RoomManager _roomManager;
        private readonly MatchingManager _matchingManager;
        public SangongMatchingService(ISangongInfoRepository repository, IMapper mapper, IMediatorHandler bus, 
            RoomManager roomManager, MatchingManager matchingManager)
        {
            _mapper = mapper;
            _bus = bus;
            _roomManager = roomManager;
            _matchingManager = matchingManager;
        }

        public async Task<BodyResponse<SangongMatchingResponseVM>> Playnow(long id)
        {
            BodyResponse<SangongMatchingResponseInfo> response = await _bus.SendCommand(new SangongPlaynowCommand(id));
            return response.MapResponse<SangongMatchingResponseVM>(_mapper);
        }

        public void SynGameRooms(SyncGameRoomMqCommand command)
        {
            _roomManager.SyncRooms(command.GameKey, command.MatchingGroup, command.SyncInfo);
        }

        public void OnJoinGameRoom(JoinGameRoomMqEvent joinEvent)
        {
            _ =_matchingManager.OnJoinGame(joinEvent.Id, joinEvent.GameKey, joinEvent.RoomId, 
                joinEvent.Blind, joinEvent.UserCount, joinEvent.MatchingGroup);
        }

        public void OnLeaveGameRoom(LeaveGameRoomMqEvent leaveEvent)
        {
            _ = _matchingManager.OnLeaveGame(leaveEvent.Id, leaveEvent.GameKey, leaveEvent.RoomId,
                leaveEvent.Blind, leaveEvent.UserCount, leaveEvent.MatchingGroup);
        }

        public Task<BaseResponse> OnUserApplySit(UserApplySitMqCommand sitcmd)
        {
            return _matchingManager.OnUserApplySit(sitcmd.Id, sitcmd.GameKey, sitcmd.Blind, sitcmd.RoomId);
        }

        public void OnUserSiteFailed(UserSitFailedMqEvent sitEvent)
        {
            _ = _matchingManager.OnSiteFailed(sitEvent.Id, sitEvent.GameKey, sitEvent.RoomId, sitEvent.MatchingGroup);
        }
    }
}
