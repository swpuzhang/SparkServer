using EasyNetQ;
using System;

namespace RabbitMqTestSender
{
    public class TextMessage
    {
        public string Text { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost;virtualHost=SkyWatch;username=SkyWatch;password=sky_watch_2019_best"))
            {
                var input = "";
                Console.WriteLine("Enter a message. 'Quit' to quit.");
                while ((input = Console.ReadLine()) != "Quit")
                {
                    bus.Publish(new TextMessage
                    {
                        Text = input
                    });
                }
            }
        }
    }
}
