using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dummy.MqCommands;
using Commons.Extenssions;
using Dummy.Application.Services;
using Commons.Domain.Models;
using Dummy.MqEvents;
using Dummy.GameMessage;

namespace Dummy.Game.WebApi.MqConsumers
{

    public class CreateRooConsumer :
        OneThreadConsumer<CreateRoomMqCommand, BodyResponse<NullBody>>
    {

        IDummyGameService _service;
        public CreateRooConsumer(IDummyGameService service)
        {
            _service = service;
        }

        public override Task<BodyResponse<NullBody>> ConsumerHandler(CreateRoomMqCommand request)
        {
            return _service.CreatRoom(request);
        }
    }

    public class JoinRoomConsummer : OneThreadConsumer<JoinGameRoomMqCommand, BodyResponse<JoinGameRoomMqResponse>>
    {
        IDummyGameService _service;
        public JoinRoomConsummer(IDummyGameService service)
        {
            _service = service;
        }
        public override Task<BodyResponse<JoinGameRoomMqResponse>> ConsumerHandler(JoinGameRoomMqCommand request)
        {
            return _service.JoinRoom(request);
        }
    }

    public class GameRoomConsummer : OneThreadConsumer<AppRoomRequest, ToAppResponse>
    {
        IDummyGameService _service;
        public GameRoomConsummer(IDummyGameService service)
        {
            _service = service;
        }
        public override async Task<ToAppResponse> ConsumerHandler(AppRoomRequest request)
        {
            return await _service.GameRoomMessage(request);
        }
    }

    public class MatchingStartedConsummer : OneThreadConsumer<MatchingStartedEvent>
    {
        IDummyGameService _service;
        public MatchingStartedConsummer(IDummyGameService service)
        {
            _service = service;
        }
        public override void ConsumerHandler(MatchingStartedEvent request)
        {
            _service.MatchingStarted(request.MatchingGroup);
        }
    }



    

}
