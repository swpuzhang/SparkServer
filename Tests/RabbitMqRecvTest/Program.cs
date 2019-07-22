using System;
using EasyNetQ;

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
            using (var bus = RabbitHutch.CreateBus("host=localhost;virtualHost=SkyWatch;username=SkyWatch;password=sky_watch_2019_best"))
            {
                bus.Subscribe<TextMessage>("test", HandleTextMessage);

                Console.WriteLine("Listening for messages. Hit <return> to quit.");
                Console.ReadLine();
            }

            
        }
        static void HandleTextMessage(TextMessage textMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got message: {0}", textMessage.Text);
            Console.ResetColor();
        }
    }
}
