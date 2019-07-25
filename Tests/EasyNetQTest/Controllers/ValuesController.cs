using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQTest.Message;
using Microsoft.AspNetCore.Mvc;
using TestMessage;

namespace EasyNetQTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private IBus _bus;

        public ValuesController(IBus bus)
        {
            _bus = bus;
        }

        // GET api/values
        [HttpGet]
        public string GetAsync()
        {
            //_bus.Publish<DoSomething>(new DoSomething { Value = "hello" });
            //_bus.Publish<EasyMessage>(new EasyMessage { Test = "hello" });
            //var reponse = await _bus.RequestAsync<DoSomething, SomethingDone>(new DoSomething { Value = "hello" });
            _bus.Send("test_send", new DoSomething { Value = "Hello Widgets!" });
            //return reponse.Value;
            return "success";
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
