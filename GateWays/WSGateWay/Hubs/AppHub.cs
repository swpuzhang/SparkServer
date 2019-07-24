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

namespace WSGateWay.Hubs
{

    public class AppHub : Hub
    {
        private readonly IRequestClient<CommonRequest> _requestClient;

        public AppHub(IRequestClient<CommonRequest> requestClient)
        {
            _requestClient = requestClient;
        }

        public async Task<CommonResponse> SendRoomMessage(CommonRequest request)
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
