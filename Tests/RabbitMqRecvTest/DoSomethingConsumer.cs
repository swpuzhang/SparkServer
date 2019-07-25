using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMessage;

namespace MassTransitTest
{
    public class DoSomethingConsumer :
        IConsumer<DoSomething>
    {
        //private IService _service;

        //public DoSomethingConsumer(IService service)
        //{
         //   _service = service;
        //}

        public async Task Consume(ConsumeContext<DoSomething> context)
        {
            // _service.SomeService();
            await Console.Out.WriteLineAsync("context: " + context.Message);
            /*await context.RespondAsync<SomethingDone>(new
            {
                Value = $"Received: {context.Message.Value}"
            });*/
        }
    }
}
