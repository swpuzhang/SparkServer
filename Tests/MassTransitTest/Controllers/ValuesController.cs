using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using TestMessage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace MassTransitTest.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<string> GetAsync()
        {
            try
            {
                //_bus.Publish<DoSomething>(new { Value = "hello world" });
                //await _bus.Publish<DoSomething>(new { Value = "hello world" });
                //var client = _bus.CreateRequestClient<DoSomething>(new Uri("rabbitmq://localhost/Test3/MassTestQueue"));

                //var response = await client.GetResponse<SomethingDone>(new { Value = "hello world"});
                //var response = await _requestClient.GetResponse<SomethingDone>(new { Value = "hello world" });
                //return ($"{response.Message.Value}, MessageId: {response.MessageId:D}");
                var client = Startup.Provider.GetService<IRequestClient<DoSomething>>();
                var response = await client.GetResponse<SomethingDone>(new { Value = "hello world" });
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
