using AutoMapper;
using Commons.Domain.Models;
using Commons.Extenssions.Defines;
using Commons.Infrastruct;
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

        private RoomIdMapGameManager _idMapper;
        private IHubContext<AppHub> _appHubContext;
        private UserConnManager _userConnManager;
        private IMapper _mapper;
        private IRpcCaller<AppHub> _rpcCaller;
        public CommandService(RoomIdMapGameManager idMapper, IHubContext<AppHub> appHubContext,
            UserConnManager userConnManager, IMapper mapper, IRpcCaller<AppHub> rpcCaller)
        {
            _idMapper = idMapper;
            _appHubContext = appHubContext;
            _userConnManager = userConnManager;
            _mapper = mapper;
            _rpcCaller = rpcCaller;
        }

        public void OnRoomGameMapConfig(RoomIdMapConfigCommand command)
        {
            _idMapper.AddMap(command.Config);
        }

        public async Task OnServerRequest(ConsumeContext<ServerRequest> context)
        {
            ServerRequest serverReq = context.Message;
            string conn = _userConnManager.GetConnByUid(serverReq.UserId);
            if (conn == null)
            {
                await context.RespondAsync<BaseResponse>(new BaseResponse(StatuCodeDefines.AppIsDisconnected,null));
                return;
            }
            ToAppRequest req = _mapper.Map<ToAppRequest>(context.Message);
            var response = await _rpcCaller.RequestCallAsync(conn, "ToAppRequest", JsonConvert.SerializeObject(req), req.Id);
            await context.RespondAsync<BaseResponse>(response);
        }
    }
}
