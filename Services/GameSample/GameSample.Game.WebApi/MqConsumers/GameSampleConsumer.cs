using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSample.MqCommands;
using Commons.Extenssions;
using GameSample.Application.Services;
using Commons.Domain.Models;
using GameSample.MqEvents;
using GameSample.GameMessage;

namespace GameSample.Game.WebApi.MqConsumers
{

    public class CreateRooConsumer :
        OneThreadConsumer<CreateRoomMqCommand, BodyResponse<NullBody>>
    {

        IGameSampleGameService _service;
        public CreateRooConsumer(IGameSampleGameService service)
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
        IGameSampleGameService _service;
        public JoinRoomConsummer(IGameSampleGameService service)
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
        IGameSampleGameService _service;
        public GameRoomConsummer(IGameSampleGameService service)
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
        IGameSampleGameService _service;
        public MatchingStartedConsummer(IGameSampleGameService service)
        {
            _service = service;
        }
        public override void ConsumerHandler(MatchingStartedEvent request)
        {
            _service.MatchingStarted(request.MatchingGroup);
        }
    }



    

}
