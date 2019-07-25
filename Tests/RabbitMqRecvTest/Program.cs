using System;
using EasyNetQ;
using MassTransit;
using MassTransitTest;
using TestMessage;

namespace RabbitMqRecvTest
{
    public class TextMessage
    {
        public string Text { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            /*using (var bus = RabbitHutch.CreateBus("host=localhost;virtualHost=SkyWatch;username=SkyWatch;password=sky_watch_2019_best"))
            {
                bus.Subscribe<TextMessage>("test", HandleTextMessage);

                Console.WriteLine("Listening for messages. Hit <return> to quit.");
                Console.ReadLine();
            }*/

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost:5672/Test2"), h =>
                {
                    h.Username("SkyWatch");
                    h.Password("sky_watch_2019_best");

                });

                //sbc.ConsumerConfigured

                sbc.ReceiveEndpoint(host, "test_queue", ep =>
                {
                    ep.Consumer<DoSomethingConsumer>();
                });

               

            });
            bus.Start();
            //var serviceAddress = new Uri("rabbitmq://localhost:5672/SkyWatch/test_queue");
            //var client = bus.CreateRequestClient<DoSomething>(serviceAddress);

            //var response = await client.GetResponse<SomethingDone>(new { Value = "Done" });
            //bus.Publish<DoSomething>(new { Value = "Hello, World." });
           
            //bus.Send(new YourMessage { Text = "Hi" });
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            bus.Stop();
            Console.WriteLine("Hello World!");


        }
        static void HandleTextMessage(TextMessage textMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got message: {0}", textMessage.Text);
            Console.ResetColor();
        }
    }
}
