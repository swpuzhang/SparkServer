using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StackExchangeRedisTest.Controllers
{
    public class Car
    {
        public Car(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        RedisHelper _redisHelper;
        public ValuesController(RedisHelper helper)
        {
            _redisHelper = helper;
        }
        // GET api/values
        [HttpGet]
        public int Get()
        {
            var onecar = new Car(100, "test", "test");
            
            _redisHelper.SetString<Car>("StackCar", onecar);
            var getcar = _redisHelper.GetString<Car>("StackCar");
            return getcar.Id;
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
