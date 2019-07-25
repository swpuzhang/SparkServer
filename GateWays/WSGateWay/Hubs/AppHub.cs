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

namespace WSGateWay.Hubs
{

    public class AppHub : Hub
    {
        private readonly IRequestClient<RoomRequest> _requestClient;
        private readonly ICommonService _commonService;
        private readonly UserConnManager _userConnManager;

        public AppHub(IRequestClient<RoomRequest> requestClient, 
            ICommonService commonService, 
            UserConnManager userConnManager)
        {
            _requestClient = requestClient;
            _commonService = commonService;
            _userConnManager = userConnManager;
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

        public BaseResponse OnLoginRequest(LoginRequest request)
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

        public async Task<CommonResponse> SendRoomMessage(RoomRequest request)
        {
            CommonResponse commonResponse = null;
            try
            {
                var busResponse = await _requestClient.GetResponse<CommonResponse>(request);
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

    }
}
