using System;
using MassTransit;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMqTest
{
    public class YourMessage
    {
        public YourMessage()
        {

        }
        [JsonConstructor]
        public YourMessage(string text)
        {
            Text = text;
        }

        public string Text { get; private set; }
    }

   

    class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost:5672/Test2"), h =>
                {
                    h.Username("SkyWatch");
                    h.Password("sky_watch_2019_best");
                  
                });

                sbc.Send<YourMessage>(x =>
                {
                    // use customerType for the routing key
                    x.UseRoutingKeyFormatter(context =>
                    {
                        Console.WriteLine("send:" + context.Message.Text);
                        return context.Message.Text;
                    });
                    
                    
                });

                sbc.Message<YourMessage>(x => x.SetEntityName("YourMessageComm"));
                sbc.Publish<YourMessage>(x => {
                    x.ExchangeType = ExchangeType.Direct;
                    Console.WriteLine($"exchange:{x.Exchange.ExchangeName} {x.Exchange.ExchangeType} {x.Exchange.Durable}");
                    
                    });
               
                sbc.ReceiveEndpoint(host, "test_queue", ep =>
                {
                    ep.BindMessageExchanges = false;

                    ep.Bind("YourMessageComm", x =>
                    {
                        x.Durable = true;
                        x.ExchangeType = "direct";
                        x.RoutingKey = "Hi";
                    });
                    ep.Handler<YourMessage>(context =>
                    {
                        return Console.Out.WriteLineAsync($"Received direct: {context.Message.Text}");
                    });
                });

                /*sbc.ReceiveEndpoint(host, "test_queue1", ep =>
                {
                    ep.Handler<YourMessage>(context =>
                    {
                        return Console.Out.WriteLineAsync($"Received: {context.Message.Text}");
                    });
                });*/

            });
            bus.Start();
            bus.Publish(new YourMessage("Hi"));
            //bus.Send(new YourMessage { Text = "Hi" });
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            bus.Stop();
            Console.WriteLine("Hello World!");
        }
    }
}
