using Commons.Domain.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MassTransit;
using Commons.Extenssions.Defines;
using WSGateWay.ViewModels;
using WSGateWay.Services;
using WSGateWay.Manager;
using Commons.Infrastruct;
using Commons.Extenssions;
using Microsoft.Extensions.Configuration;

namespace WSGateWay.Hubs
{

    public class AppHub : Hub
    {
        //private readonly IRequestClient<RoomRequest> _requestClient;
        private readonly ICommonService _commonService;
        private readonly UserConnManager _userConnManager;
        private readonly IRpcCaller<AppHub> _rpcCaller;
        private readonly IBusControl _bus;
        private IConfiguration Configuration;
        public AppHub(//IRequestClient<RoomRequest> requestClient,
            ICommonService commonService,
            UserConnManager userConnManager, IRpcCaller<AppHub> rpcCaller, 
            IBusControl bus, IConfiguration configuration)
        {
            //_requestClient = requestClient;
            _commonService = commonService;
            _userConnManager = userConnManager;
            _rpcCaller = rpcCaller;
            _bus = bus;
            Configuration = configuration;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await  base.OnDisconnectedAsync(exception);
            _userConnManager.OnDisconnected(Context.ConnectionId);
        }

        public ToAppResponse TestRequest(string test)
        {
            long uid = _userConnManager.GetUidByConn(Context.ConnectionId);
           
            return new ToAppResponse(null, StatusCodeDefines.Success, null);
        }

        public BodyResponse<NullBody> LoginRequest(LoginRequest request)
        {
            //验证token是否有效
            //如果有效将创建uid和玩家对应的关系
            var result = _commonService.TokenValidation(request.Token);
            if (!result.Key)
            {
                return new BodyResponse<NullBody>(StatusCodeDefines.LoginError, new List<string>() { "Token error relogin" });
            }
            _userConnManager.OnLogined(result.Value, Context.ConnectionId);
            
            return new BodyResponse<NullBody>(StatusCodeDefines.Success, null);
           
        }

        public async Task<ToAppResponse> RoomRequest(RoomRequest request)
        {
            ToAppResponse commonResponse = null;

            //验证是否是本人ID
            long uid = _userConnManager.GetUidByConn(Context.ConnectionId);
            if (request.Id != uid)
            {
                return new ToAppResponse(null, StatusCodeDefines.Error, null);
            }
            var busClient = _bus.CreateRequestClient<RoomRequest>(new Uri($"{Configuration["Rabbitmq:Uri"]}{request.GameRoomKey}"), TimeSpan.FromSeconds(5));
            try
            {
                var busResponse = await busClient.GetResponseExt<RoomRequest, ToAppResponse>(request);
                commonResponse = busResponse?.Message;   
            }
            catch( Exception)
            {
                
                return new ToAppResponse(null, StatusCodeDefines.BusError, null);
            }
            return commonResponse;
        }


        public void ToServerResponse(ToServerResponse response)
        {
            _rpcCaller.OnResponsed(response.MessageId);
        }
    }
}
