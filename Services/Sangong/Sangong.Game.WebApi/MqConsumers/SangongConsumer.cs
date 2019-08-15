using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sangong.MqCommands;
using Commons.Extenssions;
using Sangong.Application.Services;
using Commons.Domain.Models;
using Sangong.MqEvents;
using Sangong.GameMessage;

namespace Sangong.Game.WebApi.MqConsumers
{

    public class CreateRooConsumer :
        OneThreadConsumer<CreateRoomMqCommand, BodyResponse<NullBody>>
    {

        ISangongGameService _service;
        public CreateRooConsumer(ISangongGameService service)
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
        ISangongGameService _service;
        public JoinRoomConsummer(ISangongGameService service)
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
        ISangongGameService _service;
        public GameRoomConsummer(ISangongGameService service)
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
        ISangongGameService _service;
        public MatchingStartedConsummer(ISangongGameService service)
        {
            _service = service;
        }
        public override void ConsumerHandler(MatchingStartedEvent request)
        {
            _service.MatchingStarted(request.MatchingGroup);
        }
    }



    

}
