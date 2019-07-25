using EasyNetQ;
using MassTransit;
using System;
using System.Threading.Tasks;
using TestMessage;

namespace RabbitMqTestSender
{
    public class TextMessage
    {
        public string Text { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            /*using (var bus = RabbitHutch.CreateBus("host=localhost;virtualHost=SkyWatch;username=SkyWatch;password=sky_watch_2019_best"))
            {
                bus.Subscribe<TextMessage>("test", HandleTextMessage);

                Console.WriteLine("Listening for messages. Hit <return> to quit.");
                Console.ReadLine();
            }*/

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost:5672/SkyWatch"), h =>
                {
                    h.Username("SkyWatch");
                    h.Password("sky_watch_2019_best");

                });

             
            });
            bus.Start();
            var serviceAddress = new Uri("rabbitmq://localhost:5672/SkyWatch/test_queue");
            var client = bus.CreateRequestClient<DoSomething>(serviceAddress);

            var response = await client.GetResponse<SomethingDone>(new { Value = "Done" });
            Console.WriteLine(response.Message); ;
            //bus.Publish<DoSomething>(new { Value = "Hello, World." });
            //bus.Send(new YourMessage { Text = "Hi" });
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            bus.Stop();
            Console.WriteLine("Hello World!");


        }
    }
}
