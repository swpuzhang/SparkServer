﻿using Commons.Domain.Models;
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

}