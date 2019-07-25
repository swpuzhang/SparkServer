using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMessage;
using Microsoft.Extensions.DependencyInjection;

namespace MassTransitTest
{
    public class DoSomethingConsumer :
        IConsumer<DoSomething>
    {
        private IServiceProvider _provider;
        IService _service;
        public DoSomethingConsumer(IService service)
        {
            _provider = Startup.Provider;
            _service = service;
        }

        public async Task Consume(ConsumeContext<DoSomething> context)
        {
            /*using(IServiceScope scope = _provider.CreateScope())
            {
                IService service = scope.ServiceProvider.GetService<IService>();
                service.SomeService();
                await Console.Out.WriteLineAsync("context: " + context.Message);
                
            }*/
            _service.SomeService();
            await context.RespondAsync<SomethingDone>(new
            {
                Value = $"Received: {context.Message.Value}"
            });
        }
    }

    
}
