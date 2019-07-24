using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Commons.Domain.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace SignalRTest
{

    public class RoomRequest
    {
        public RoomRequest(int roomId, int userId, string requestName)
        {
            RoomId = roomId;
            UserId = userId;
            RequestName = requestName;
        }

        [Required]
        public int RoomId { get; private set; }

        [Required]
        public int UserId { get; private set; }

        [Required]
        public string RequestName { get; private set; }
    }

    public class EnterRoomreq
    {
        public EnterRoomreq(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    public class GameRequest<T> : RoomRequest
    {
        public GameRequest(int roomId, int userId, string requestName, T request)
            : base(roomId, userId, requestName)
        {
            this.request = request;
        }

        public T request { get; private set; }
    }

    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/AppHub")
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                })
                .Build();
            await connection.StartAsync();

            Console.WriteLine("Starting connection. Press Ctrl-C to close.");
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, a) =>
            {
                a.Cancel = true;
                cts.Cancel();
            };

            connection.Closed += e =>
            {
                Console.WriteLine("Connection closed with error: {0}", e);

                cts.Cancel();
                return Task.CompletedTask;
            };

            BaseResponse response = await connection.InvokeAsync<BaseResponse>("SendMessage", new RoomRequest(1,  1, null ));
            Console.WriteLine(response.StatusCode);
        }
    }
}
