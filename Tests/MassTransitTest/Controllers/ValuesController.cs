using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using TestMessage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Commons.Domain.Models;

namespace MassTransitTest.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IBusControl _bus;
        IRequestClient<DoSomething> _requestClient;
        public ValuesController(IBusControl bus, IRequestClient<DoSomething> client)
        {
           // _requestClient = requestClient;
            _bus = bus;
            _requestClient = client;
        }

        /* public ValuesController(IBusControl bus)
         {
             _bus = bus;
         }*/
        // GET api/values
        [HttpGet]
        public string GetAsync( int Id)
        {
            Console.WriteLine(HttpContext.Request.Method);
            Console.WriteLine(HttpContext.Request.Path);
            Console.WriteLine(Id);
            try
            {
                _bus.Publish<ServerRequest1<DoSomething>>(new ServerRequest1<DoSomething>(new DoSomething() { Value = "DoSomething" }));
                _bus.Publish<ServerRequest1<DoMessage>>(new ServerRequest1<DoMessage>(new DoMessage() { Value = "DoMessage" }));
                //_bus.Publish<DoSomething>(new { Value = "hello world" });
                //_bus.Publish<DoMessage>(new { Value = "hello world" });
                //await _bus.Publish<DoSomething>(new { Value = "hello world" });
                //var client = _bus.CreateRequestClient<DoSomething>(new Uri("rabbitmq://localhost/Test3/MassTestQueue"));

                //var response = await client.GetResponse<SomethingDone>(new { Value = "hello world"});
                //var response = await _requestClient.GetResponse<SomethingDone>(new { Value = "hello world" });
                //return ($"{response.Message.Value}, MessageId: {response.MessageId:D}");
                /* var client = Startup.Provider.GetService<IRequestClient<DoSomething>>();
                 var response = await client.GetResponse<SomethingDone>(new { Value = "hello world" });

                 await client.GetResponse<SomethingDone>(new { Value = "hello world" });
                 await client.GetResponse<SomethingDone>(new { Value = "hello world" });
                 await client.GetResponse<SomethingDone>(new { Value = "hello world" });
                 await client.GetResponse<SomethingDone>(new { Value = "hello world" });
                 await client.GetResponse<SomethingDone>(new { Value = "hello world" });*/
                /*int i = 100;
                while (i-- > 0)
                {
                    var response = await client.GetResponse<SomethingDone>(new { Value = "hello world" });
                    await Task.Delay(100);
                }*/


                return "success";
            }
            catch (RequestTimeoutException)
            {
                return "error";
            }
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
