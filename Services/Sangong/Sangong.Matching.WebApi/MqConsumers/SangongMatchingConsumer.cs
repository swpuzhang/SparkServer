using Commons.Domain.Models;
using MassTransit;
using Sangong.Application.Services;
using Sangong.MqCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sangong.MqEvents;
using Commons.Extenssions;

namespace Sangong.Matching.WebApi.MqConsumers
{
    public class SyncGameRoomConsumer :
        OneThreadConsumer<SyncGameRoomMqCommand>
    {
        ISangongMatchingService _service;

        public SyncGameRoomConsumer(ISangongMatchingService service)
        {
            _service = service;
        }

        public override void ConsumerHandler(SyncGameRoomMqCommand request)
        {
            _service.SynGameRooms(request);
        }
    }

    public class JoinGameRoomConsumer :
        OneThreadConsumer<JoinGameRoomMqEvent>
    {
        ISangongMatchingService _service;

        public JoinGameRoomConsumer(ISangongMatchingService service)
        {
            _service = service;
        }

        public override void ConsumerHandler(JoinGameRoomMqEvent request)
        {
            _service.OnJoinGameRoom(request);
        }
    }

    public class LeaveGameRoomConsumer :
        OneThreadConsumer<LeaveGameRoomMqEvent>
    {
        ISangongMatchingService _service;

        public LeaveGameRoomConsumer(ISangongMatchingService service)
        {
            _service = service;
        }

        public override void ConsumerHandler(LeaveGameRoomMqEvent request)
        {
            _service.OnLeaveGameRoom(request);
        }
    }


    public class UserApplySitConsumer :
        OneThreadConsumer<UserApplySitMqCommand, BaseResponse>
    {
        ISangongMatchingService _service;

        public UserApplySitConsumer(ISangongMatchingService service)
        {
            _service = service;
        }


        public async override Task<BaseResponse> ConsumerHandler(UserApplySitMqCommand request)
        {
            return await _service.OnUserApplySit(request);
        }
    }

    public class UserSitFailedConsumer :
       OneThreadConsumer<UserSitFailedMqEvent>
    {
        ISangongMatchingService _service;

        public UserSitFailedConsumer(ISangongMatchingService service)
        {
            _service = service;
        }


        

        public override void ConsumerHandler(UserSitFailedMqEvent request)
        {
            _service.OnUserSiteFailed(request);
        }
    }


}
