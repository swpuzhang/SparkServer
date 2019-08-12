using Commons.Domain.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Commons.Extenssions.Defines;
using System.Threading;

namespace Commons.Infrastruct
{
    public class RpcCaller<THub> : IRpcCaller<THub> where THub : Hub
    {
       // private readonly ConcurrentDictionary<Guid, KeyValuePair<TaskCompletionSource<BodyResponse<NullBody>>, Timer>> _pendingMethodCalls = 
        //    new ConcurrentDictionary<Guid, KeyValuePair<TaskCompletionSource<BodyResponse<NullBody>>, Timer>>();

        private readonly ConcurrentDictionary
            <Guid,KeyValuePair<TaskCompletionSource<BodyResponse<NullBody>>,CancellationTokenSource>> _Calls =
            new ConcurrentDictionary<Guid, KeyValuePair<TaskCompletionSource<BodyResponse<NullBody>>, CancellationTokenSource>>();


        private readonly IHubContext<THub> _hubContext;
 
        public RpcCaller(IHubContext<THub> hubContext)
        {
            //_token.
            _hubContext = hubContext;
        }

        public void OnResponsed(Guid id)
        {
            /*if (_pendingMethodCalls.TryRemove(id, out var value))
            {
                value.Value.Dispose();
                value.Key.TrySetResult(new BodyResponse<NullBody>(StatuCodeDefines.Success, null));
            }*/

            if (_Calls.TryRemove(id, out var value))
            {
                value.Key.TrySetResult(new BodyResponse<NullBody>(StatuCodeDefines.Success, null));
                value.Value.Dispose();
            }

        }

        /*public void OnTimeOut(Object o)
        {
            Guid id = (Guid)o;
            if (_pendingMethodCalls.TryRemove(id, out var value))
            {
                value.Value.Dispose();
                value.Key.TrySetResult(new BodyResponse<NullBody>(StatuCodeDefines.Timeout, new List<string>() { "timeout" }));
            }
        }*/

        /*public async Task<BodyResponse<NullBody>> RequestCallAsync(string conn, string method, 
            string reqData, Guid id, int waitMiliSeconds = 5000)
        {
   
            TaskCompletionSource<BodyResponse<NullBody>> methodCallCompletionSource = new TaskCompletionSource<BodyResponse<NullBody>>();
            var timer = new Timer(OnTimeOut, id, waitMiliSeconds,Timeout.Infinite);
            
            if (_pendingMethodCalls.TryAdd(id, new KeyValuePair<TaskCompletionSource<BodyResponse<NullBody>>, Timer>(methodCallCompletionSource, timer)))
            {
                try
                {
                    await _hubContext.Clients.Client(conn).SendAsync(method, reqData);
                }
               
                catch (Exception ex)
                {
                    if (_pendingMethodCalls.TryRemove(id, out var value))
                    {
                        value.Value.Dispose();
                    }
                    return new BodyResponse<NullBody>(StatuCodeDefines.AppIsDisconnected, new List<string>() { ex.Message });
                }
                
                return await methodCallCompletionSource.Task;
            }
            timer.Dispose();
            return new BodyResponse<NullBody>(StatuCodeDefines.GuidError, new List<string>() { StatuCodeDefines.GuidError.ToString() });
        }*/

        public async Task<BodyResponse<NullBody>> RequestCallAsync(string conn, string method,
            string reqData, Guid id, int waitMiliSeconds = 5000)
        {

            TaskCompletionSource<BodyResponse<NullBody>> methodCallCompletionSource = new TaskCompletionSource<BodyResponse<NullBody>>();
            CancellationTokenSource token = new CancellationTokenSource(waitMiliSeconds);
            if (_Calls.TryAdd(id, new KeyValuePair<TaskCompletionSource<BodyResponse<NullBody>>,
                CancellationTokenSource>(methodCallCompletionSource, token)))
            {
                try
                {
                    using (token.Token.Register(
                        () =>
                        {
                            if (_Calls.TryRemove(id, out var value))
                            {
                                value.Key.TrySetResult(new BodyResponse<NullBody>(StatuCodeDefines.Timeout, new List<string>() { "timeout" }));
                                value.Value.Dispose();
                            }
                        }
                        ))
                    {
                        Excute(conn, method, reqData, id);
                        return await methodCallCompletionSource.Task;
                    }  
                }

                catch (Exception ex)
                {
                    if (_Calls.TryRemove(id, out var value))
                    {
                        value.Value.Dispose();
                    }
                    return new BodyResponse<NullBody>(StatuCodeDefines.AppIsDisconnected, new List<string>() { ex.Message });
                }
               
            }

            else
            {
                token.Dispose();
                return new BodyResponse<NullBody>(StatuCodeDefines.GuidError, new List<string>() { StatuCodeDefines.GuidError.ToString() });
            }
            
        }

        private void Excute(string conn, string method, string data, Guid id)
        {
            Task.Run(async () =>
            {
                try
                {
                    await _hubContext.Clients.Client(conn).SendAsync(method, data);
                }
                catch (Exception ex)
                {
                    if (_Calls.TryRemove(id, out var value))
                    {
                        
                        value.Key.TrySetResult(new BodyResponse<NullBody>(StatuCodeDefines.AppIsDisconnected, new List<string>() { ex.Message }));
                        value.Value.Dispose();

                    }
                }
            });
            
        }
    }
}
