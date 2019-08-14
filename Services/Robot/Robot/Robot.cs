using Account.Application.ViewModels;
using Commons.Domain.Models;
using Commons.Extenssions.Defines;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using WSGateWay.ViewModels;
using Common.Signalr.Client;
using Sangong.Application.ViewModels;

namespace Robot
{

    public  enum RobotStatus
    {
        Init,
        Logined
    }

    public class Robot :IDisposable
    {


        public Robot(long id, string userName, string platformId, int sex, string headUrl)
        {
            _id = id;
            _userName = userName;
            _platformId = platformId;
            _sex = sex;
            _headUrl = headUrl;
            _wsClient = new SignalrClient();
        }
       
        
        public async Task<bool> LoginAsync(string loginHost)
        {
            
            _client.BaseAddress = new Uri(loginHost);
            var response = await _client.PostAsync("/api/Account/Login", new StringContent(JsonConvert.SerializeObject(new AccountVM(_platformId, _userName, 0,
                _headUrl, 0)), Encoding.UTF8, mediaType: "application/json"));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return false;
            }
            var accountInfo = JsonConvert.DeserializeObject<BodyResponse<AccountResponseVM>>(await response.Content.ReadAsStringAsync());
            if (accountInfo.StatusCode != StatuCodeDefines.Success)
            {
                return false;
            }
            //登录websocket

            _token = accountInfo.Body.Token;
            var wsresponse = await _wsClient.Connect(accountInfo.Body.LongConnectHost, true, accountInfo.Body.Token, () => Task.CompletedTask, (err) => Task.CompletedTask);
            if (wsresponse.StatusCode != StatuCodeDefines.Success)
            {
                return false;
            }
           

            return true;
                
        }

        public async Task<BodyResponse<MatchingResponseVM>> MatchGameAsync()
        {
            _client.DefaultRequestHeaders.Add("Token", _token);
             var response = await _client.GetAsync("/api/SangongMatching/PlayNow");
           
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<BodyResponse<MatchingResponseVM>>(await response.Content.ReadAsStringAsync());
        }

        public void Dispose()
        {
            _wsClient.Dispose();
        }

        private long _id;
        private string _userName;
        private string _platformId;
        private int _sex;
        private string _headUrl;
        private string _token;
        SignalrClient _wsClient;
        HttpClient _client = new HttpClient();
    }
}
