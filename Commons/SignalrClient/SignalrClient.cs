using Commons.Domain.Models;
using Commons.Extenssions.Defines;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Common.Signalr.Client
{
    public class SignalrClient : IDisposable
    {
        private HubConnection _hubConnection;
        private string _remoteUri;
        private bool _isAutoReconn;
        private Func<Exception, Task> __closeHandler;
        private Func<Task> _connectedHandler;
        private string _loginToken;

        public async Task<BodyResponse<NullBody>> Connect(string remoteUri, bool isAutoReconn, string loginToken, Func<Task> ConnectedHandler, Func<Exception, Task> CloseHandler)
        {
            _remoteUri = remoteUri;
            _isAutoReconn = isAutoReconn;
            _loginToken = loginToken;
            __closeHandler = CloseHandler;
            _connectedHandler = ConnectedHandler;
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_remoteUri, options =>
                {
                    options.SkipNegotiation = true;
                    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
                })
                .Build();
            _hubConnection.Closed += async (error) => 
            {
                await CloseHandler(error);
                if (_isAutoReconn)
                {
                    try
                    {
                        await _hubConnection.StartAsync();
                        await ConnectedHandle();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                }
            };

            await _hubConnection.StartAsync();
            return await ConnectedHandle();
           
        }
        
        public async Task<BodyResponse<NullBody>> ConnectedHandle()
        {
            
            var response = await _hubConnection.InvokeAsync<BodyResponse<NullBody>>("LoginRequest", new { Token = _loginToken });
            if (response.StatusCode != StatusCodeDefines.Success)
            {
                _isAutoReconn = false;
            }
            await _connectedHandler();
            return response;
        }

        public async void Dispose()
        {
            await _hubConnection.DisposeAsync();
        }

        public async Task<ToAppResponse> SendRoomRequest(RoomRequest request)
        {
            var response = await _hubConnection.InvokeAsync<ToAppResponse>("RoomRequest", request);
            return response;
        }

        public async Task<ToAppResponse> SendRoomRequest<TReuqest>(long id, string gameKey, string roomId, TReuqest request)
            where TReuqest :class
        {
            var roomRequest = new RoomRequest(id, typeof(TReuqest).Name, JsonConvert.SerializeObject(request), Guid.NewGuid(), gameKey, roomId);
            return await SendRoomRequest(roomRequest);
        }
    }
}
