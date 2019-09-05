using Commons.Domain.Models;
using MassTransit;
using Dummy.Application.Services;
using Dummy.MqCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dummy.MqEvents;
using Commons.Extenssions;

namespace Dummy.Matching.WebApi.MqConsumers
{
    public class SyncGameRoomConsumer :
        OneThreadConsumer<SyncGameRoomMqCommand>
    {
        IDummyMatchingService _service;

        public SyncGameRoomConsumer(IDummyMatchingService service)
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
        IDummyMatchingService _service;

        public JoinGameRoomConsumer(IDummyMatchingService service)
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
        IDummyMatchingService _service;

        public LeaveGameRoomConsumer(IDummyMatchingService service)
        {
            _service = service;
        }

        public override void ConsumerHandler(LeaveGameRoomMqEvent request)
        {
            _service.OnLeaveGameRoom(request);
        }
    }


    public class UserApplySitConsumer :
        OneThreadConsumer<UserApplySitMqCommand, BodyResponse<NullBody>>
    {
        IDummyMatchingService _service;

        public UserApplySitConsumer(IDummyMatchingService service)
        {
            _service = service;
        }


        public async override Task<BodyResponse<NullBody>> ConsumerHandler(UserApplySitMqCommand request)
        {
            return await _service.OnUserApplySit(request);
        }
    }

    public class UserSitFailedConsumer :
       OneThreadConsumer<UserSitFailedMqEvent>
    {
        IDummyMatchingService _service;

        public UserSitFailedConsumer(IDummyMatchingService service)
        {
            _service = service;
        }


        

        public override void ConsumerHandler(UserSitFailedMqEvent request)
        {
            _service.OnUserSiteFailed(request);
        }
    }


}
