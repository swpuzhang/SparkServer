using System;
using MassTransit;
namespace RabbitMqTest
{
    public class YourMessage
    {
        public string Text { get; set; }
    }

   

    class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost/SkyWatch"), h =>
                {
                    h.Username("SkyWatch");
                    h.Password("sky_watch_2019_best");
                  
                });

                sbc.ReceiveEndpoint(host, "test_queue", ep =>
                {
                    ep.Handler<YourMessage>(context =>
                    {
                        return Console.Out.WriteLineAsync($"Received: {context.Message.Text}");
                    });
                });

                sbc.ReceiveEndpoint(host, "test_queue1", ep =>
                {
                    ep.Handler<YourMessage>(context =>
                    {
                        return Console.Out.WriteLineAsync($"Received: {context.Message.Text}");
                    });
                });

            });
            bus.Start();
            bus.Publish(new YourMessage { Text = "Hi" });
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            bus.Stop();
            Console.WriteLine("Hello World!");
        }
    }
}
