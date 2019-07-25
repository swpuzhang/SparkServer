using Commons.Domain.Events;
using EasyNetQ;
using EasyNetQTest.Message;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestMessage;

namespace EasyNetQTest
{
    public class BusService :
        IHostedService
    {
        private readonly IBus _busControl;

        public BusService(IBus busControl)
        {
            _busControl = busControl;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            _busControl.Subscribe<DoSomething>("test", HandleTextMessage);
            _busControl.Subscribe<EasyMessage>("easy", HandleEasynMessage);
            _busControl.Respond<DoSomething, SomethingDone>(request => new SomethingDone { Value = "Responding to " + request.Value });
            _busControl.Receive<DoSomething>("test_send1", HandleRecvMessage);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }

        static void HandleTextMessage(DoSomething textMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got message: {0}", textMessage.Value);
            Console.ResetColor();
        }

        static void HandleEasynMessage(EasyMessage textMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got message: {0}", textMessage.Test);
            Console.ResetColor();
        }

        static void HandleRecvMessage(DoSomething textMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got message: {0}", textMessage.Value);
            Console.ResetColor();
        }
    }
}
