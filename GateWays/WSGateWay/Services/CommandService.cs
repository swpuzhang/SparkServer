using AutoMapper;
using Commons.Domain.Models;
using GameSangong.Domain.MqCommands;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSGateWay.Hubs;
using WSGateWay.Manager;

namespace WSGateWay.Services
{
    public class CommandService : ICommandService
    {

        private RoomIdMapGame _idMapper;
        private IHubContext<AppHub> _appHubContext;
        private UserConnManager _userConnManager;
        private IMapper _mapper;
        public CommandService(RoomIdMapGame idMapper, IHubContext<AppHub> appHubContext, 
            UserConnManager userConnManager, IMapper mapper)
        {
            _idMapper = idMapper;
            _appHubContext = appHubContext;
            _userConnManager = userConnManager;
            _mapper = mapper;
        }

        public void OnRoomGameMapConfig(RoomIdMapConfigCommand command)
        {
            _idMapper.AddMap(command.Config);
        }

        public async Task OnServerRequest(ConsumeContext<ServerRequest> context)
        {
            ServerRequest serverReq = context.Message;
            ToAppRequest req = _mapper.Map<ToAppRequest>(context.Message);
            await _appHubContext.Clients.Client("").SendAsync("ToAppRequest", JsonConvert.SerializeObject(req));
        }
    }
}
