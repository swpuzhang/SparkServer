using AutoMapper;
using Commons.Domain.Models;
using Commons.Extenssions.Defines;
using Commons.Infrastruct;
using Sangong.MqCommands;
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
        private readonly IHubContext<AppHub> _appHubContext;
        private readonly UserConnManager _userConnManager;
        private readonly IMapper _mapper;
        public CommandService(IHubContext<AppHub> appHubContext,
            UserConnManager userConnManager, IMapper mapper)
        {

            _appHubContext = appHubContext;
            _userConnManager = userConnManager;
            _mapper = mapper;
        }

        public Task OnGameRoomRequest(ConsumeContext<GameServerRequest> context)
        {
            GameServerRequest serverReq = context.Message;
            string conn = _userConnManager.GetConnByUid(serverReq.Id);
            if (conn == null)
            {
                return Task.CompletedTask;
            }
            ToAppRoomRequest req = _mapper.Map<ToAppRoomRequest>(serverReq);
            return _appHubContext.Clients.Clients(conn).SendAsync("ToAppRoomRequest", req);
        }

        public  Task OnServerRequest(ConsumeContext<ServerRequest> context)
        {
            ServerRequest serverReq = context.Message;
            string conn = _userConnManager.GetConnByUid(serverReq.Id);
            if (conn == null)
            {
                return Task.CompletedTask;
            }
            ToAppRequest req = _mapper.Map<ToAppRequest>(context.Message);
            return _appHubContext.Clients.Clients(conn).SendAsync("ToAppRequest", req);
            //var response = await _rpcCaller.RequestCallAsync(conn, "ToAppRequest", JsonConvert.SerializeObject(req), req.MessageId);
            //await context.RespondAsync<BodyResponse<NullBody>>(response);
        }
    }
}
