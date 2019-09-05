using Commons.Domain.Models;
using MassTransit;
using GameSample.Application.Services;
using GameSample.MqCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSample.MqEvents;
using Commons.Extenssions;

namespace GameSample.Matching.WebApi.MqConsumers
{
    public class SyncGameRoomConsumer :
        OneThreadConsumer<SyncGameRoomMqCommand>
    {
        IGameSampleMatchingService _service;

        public SyncGameRoomConsumer(IGameSampleMatchingService service)
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
        IGameSampleMatchingService _service;

        public JoinGameRoomConsumer(IGameSampleMatchingService service)
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
        IGameSampleMatchingService _service;

        public LeaveGameRoomConsumer(IGameSampleMatchingService service)
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
        IGameSampleMatchingService _service;

        public UserApplySitConsumer(IGameSampleMatchingService service)
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
        IGameSampleMatchingService _service;

        public UserSitFailedConsumer(IGameSampleMatchingService service)
        {
            _service = service;
        }


        

        public override void ConsumerHandler(UserSitFailedMqEvent request)
        {
            _service.OnUserSiteFailed(request);
        }
    }


}
