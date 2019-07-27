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

namespace WSGateWay.Hubs
{

    public class AppHub : Hub
    {
        //private readonly IRequestClient<RoomRequest> _requestClient;
        private readonly ICommonService _commonService;
        private readonly UserConnManager _userConnManager;
        private readonly IRpcCaller<AppHub> _rpcCaller;
        private RoomIdMapGameManager _roomidMapper;
        private readonly IBusControl _bus;
        public AppHub(//IRequestClient<RoomRequest> requestClient,
            ICommonService commonService,
            UserConnManager userConnManager, IRpcCaller<AppHub> rpcCaller,
            RoomIdMapGameManager roomidMapper, IBusControl bus)
        {
            //_requestClient = requestClient;
            _commonService = commonService;
            _userConnManager = userConnManager;
            _rpcCaller = rpcCaller;
            _roomidMapper = roomidMapper;
            _bus = bus;
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

        public BaseResponse LoginRequest(LoginRequest request)
        {
            //验证token是否有效
            //如果有效将创建uid和玩家对应的关系
            var result = _commonService.TokenValidation(request.Token);
            if (!result.Key)
            {
                return new BaseResponse(StatuCodeDefines.LoginError, new List<string>() { "Token error relogin" });
            }
            _userConnManager.OnLogined(result.Value, Context.ConnectionId);
            return new BaseResponse(StatuCodeDefines.Success, null);
           
        }

        public async Task<CommonResponse> RoomRequest(RoomRequest request)
        {
            CommonResponse commonResponse = null;
            string gameKey =  _roomidMapper.GetGameByRoomid(request.RoomId);
            var busClient = _bus.CreateRequestClient<RoomRequest>(new Uri($"{Startup.mqConnectionStr}/{gameKey}"), TimeSpan.FromSeconds(5));
            try
            {
                var busResponse = await busClient.GetResponse<CommonResponse>(request);
                commonResponse = busResponse?.Message;   
            }
            catch( Exception)
            {

            }
            if (commonResponse == null)
            {
                var response = new BaseResponse((StatuCodeDefines.BusError), null);
                return new CommonResponse(JsonConvert.SerializeObject(response), request.Id);
            }

            return commonResponse;
        }


        public void CommonResponse(CommonResponse response)
        {
            _rpcCaller.OnResponsed(response.Id);
        }
    }
}
