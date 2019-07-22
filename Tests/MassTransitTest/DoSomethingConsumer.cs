using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MassTransitTest
{
    public class DoSomethingConsumer :
        IConsumer<DoSomething>
    {
        

        public DoSomethingConsumer()
        {
           
        }

        public async Task Consume(ConsumeContext<DoSomething> context)
        {
            

            await context.RespondAsync<SomethingDone>(new
            {
                Value = $"Received: {context.Message.Value}"
            });
        }
    }
}
