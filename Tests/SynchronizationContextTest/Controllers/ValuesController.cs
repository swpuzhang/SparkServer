using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneThread;

namespace SynchronizationContextTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            OneThreadSynchronizationContext.Instance.Post(Test1, null);
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            OneThreadSynchronizationContext.Instance.Post(Test2, null);
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

        public static Dictionary<int, string> dic = new Dictionary<int, string>();

        public static async  void Test1(object obj)
        {
            Console.WriteLine($"begin Test1 cur thread{Thread.CurrentThread.ManagedThreadId}------------");
            var res1 = Task.Run(() =>
            {
                Console.WriteLine($"in Task cur thread{Thread.CurrentThread.ManagedThreadId}------------");
                Task.Delay(10000).Wait();
                Console.WriteLine($"in Task  cur thread{Thread.CurrentThread.ManagedThreadId}------------");
                return 1;
            });
            var res2 = Task.Run(() =>
            {
                Console.WriteLine($"in Task cur thread{Thread.CurrentThread.ManagedThreadId}------------");
                Task.Delay(10000).Wait();
                Console.WriteLine($"in Task  cur thread{Thread.CurrentThread.ManagedThreadId}------------");
                return 2.0;
            });
            await Task.WhenAll(res1, res2);

            Console.WriteLine($"await cur thread{Thread.CurrentThread.ManagedThreadId}------------{res1.Result} {res2.Result}");

            await Task.Run(() =>
            {
                Console.WriteLine($"in Task cur thread{Thread.CurrentThread.ManagedThreadId}------------");
                Task.Delay(10000).Wait();
                Console.WriteLine($"in Task cur thread{Thread.CurrentThread.ManagedThreadId}------------");
            }
            );
            Console.WriteLine($"await cur thread{Thread.CurrentThread.ManagedThreadId}------------");
        }

        public static async void Test2(object obj)
        {
            HostedService._timer.Stop();
            Console.WriteLine($"begin Test2 cur thread{Thread.CurrentThread.ManagedThreadId}------------");
            await Task.Run(() =>
            {
                Console.WriteLine($"in Task cur thread{Thread.CurrentThread.ManagedThreadId}------------");
                Task.Delay(2000).Wait();
                Console.WriteLine($"in Task cur thread{Thread.CurrentThread.ManagedThreadId}------------");
            }
            );
            Console.WriteLine($"await cur thread{Thread.CurrentThread.ManagedThreadId}------------");

            await Task.Run(() =>
            {
                Console.WriteLine($"in Task cur thread{Thread.CurrentThread.ManagedThreadId}------------");
                Task.Delay(2000).Wait();
                Console.WriteLine($"in Task cur thread{Thread.CurrentThread.ManagedThreadId}------------");
            }
            );
            Console.WriteLine($"await cur thread{Thread.CurrentThread.ManagedThreadId}------------");
        }
    }
}
