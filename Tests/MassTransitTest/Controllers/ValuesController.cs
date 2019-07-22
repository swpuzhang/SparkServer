using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IRequestClient<DoSomething> _requestClient;
        public ValuesController(IRequestClient<DoSomething> requestClient)
        {
            _requestClient = requestClient;
        }
        // GET api/values
        [HttpGet]
        public async Task<string> GetAsync()
        {
            try
            {
                var request = _requestClient.Create(new { Value = "Hello, World." });

                var response = await request.GetResponse<SomethingDone>();

                return ($"{response.Message.Value}, MessageId: {response.MessageId:D}");
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
