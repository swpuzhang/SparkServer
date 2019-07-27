using Commons.Domain.Models;
using GameSangong.Domain.MqCommands;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSGateWay.Services
{
    public interface ICommandService
    {

        void OnRoomGameMapConfig(RoomIdMapConfigCommand command);
        Task OnServerRequest(ConsumeContext<ServerRequest> context);
       
    }
}
