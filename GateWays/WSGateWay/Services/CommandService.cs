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
        private IHubContext<AppHub> _appHubContext;
        private UserConnManager _userConnManager;
        private IMapper _mapper;
        private IRpcCaller<AppHub> _rpcCaller;
        public CommandService(IHubContext<AppHub> appHubContext,
            UserConnManager userConnManager, IMapper mapper, IRpcCaller<AppHub> rpcCaller)
        {

            _appHubContext = appHubContext;
            _userConnManager = userConnManager;
            _mapper = mapper;
            _rpcCaller = rpcCaller;
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
            _appHubContext.Clients.Clients(conn).SendAsync("ToAppRoomRequest");
            return Task.CompletedTask;
        }

        public async Task OnServerRequest(ConsumeContext<ServerRequest> context)
        {
            ServerRequest serverReq = context.Message;
            string conn = _userConnManager.GetConnByUid(serverReq.Id);
            if (conn == null)
            {
                await context.RespondAsync<BaseResponse>(new BaseResponse(StatuCodeDefines.AppIsDisconnected,null));
                return;
            }
            ToAppRequest req = _mapper.Map<ToAppRequest>(context.Message);
            var response = await _rpcCaller.RequestCallAsync(conn, "ToAppRequest", JsonConvert.SerializeObject(req), req.MessageId);
            await context.RespondAsync<BaseResponse>(response);
        }
    }
}
