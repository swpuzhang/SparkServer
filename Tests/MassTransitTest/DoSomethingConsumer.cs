using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMessage;
using Microsoft.Extensions.DependencyInjection;
using Commons.Domain.Models;

namespace MassTransitTest
{
    public class DoSomethingConsumer :
        IConsumer<DoSomething>, IConsumer<DoMessage>
    {
        private IServiceProvider _provider;
        IService _service;
        public DoSomethingConsumer(IService service)
        {
            _provider = Startup.Provider;
            _service = service;
        }

        public  Task Consume(ConsumeContext<DoSomething> context)
        {
            /*using(IServiceScope scope = _provider.CreateScope())
            {
                IService service = scope.ServiceProvider.GetService<IService>();
                service.SomeService();
                await Console.Out.WriteLineAsync("context: " + context.Message);
                
            }*/
            //_service.SomeService();
            /*await context.RespondAsync<SomethingDone>(new
            {
                Value = $"Received: {context.Message.Value}"
            });*/
            Console.WriteLine("DoSomething comsumer");
            return Task.CompletedTask;

        }

        public Task Consume(ConsumeContext<DoMessage> context)
        {
            Console.WriteLine("DoMessage comsumer");
            return Task.CompletedTask;
        }
    }

    public class ServerRequestConsumer<T> :
        IConsumer<ServerRequest1<T>> where T : class
    {

        public Task Consume(ConsumeContext<ServerRequest1<T>> context)
        {
            Console.WriteLine("ServerRequest1 comsumer");
            return Task.CompletedTask;
        }
    }

}
