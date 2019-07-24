using System;
using System.Reflection;
using Newtonsoft.Json;

namespace ReflectTest
{
    public class TestRequest
    {
        public TestRequest()
        {

        }

        public TestRequest(string reqestName, string data)
        {
            ReqestName = reqestName;
            Data = data;
        }

        public string ReqestName { get; private set; }
        public string Data { get; private set; }
    }

    public class Request1
    {
        public Request1()
        {
        }

        [JsonConstructor]
        public Request1(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
    }

    public class Request2
    {
        public Request2()
        {
        }

        [JsonConstructor]
        public Request2(int id, string name, string address)
        {
            Id = id;
            Name = name ;
            Address = address;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }

       
        public string Address { get; private set; } = "";
    }

    public class RequestHandlers
    {
        public void OnRequest1(Request1 req)
        {
            Console.WriteLine($"OnRequest1 {req.Id} {req.Name}");
        }

        public void OnRequest2(Request2 req)
        {
            Console.WriteLine($"OnRequest2 {req.Id} {req.Name} {req.Address}");
        }
    }

    public static class Handlers
    {
        public static void OnRequest2(Request2 req)
        {
            Console.WriteLine($"OnRequest2 {req.Id} {req.Name} {req.Address}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Request1 r1 = new Request1(1, "zhangyang");
            string jsonStr = JsonConvert.SerializeObject(r1);
            TestRequest tr = new TestRequest(typeof(Request1).Name, jsonStr);
            
            Console.WriteLine($"{tr.ReqestName} {tr.Data}");

            Type t = Type.GetType($"ReflectTest.{tr.ReqestName}");
            Type t2 = Type.GetType($"ReflectTest.Request2");
            //Request1 rc1 = JsonConvert.DeserializeObject<Request1>(tr.Data);
            var thandler = typeof(RequestHandlers).GetMethod($"On{tr.ReqestName}");
            var thandler2 = typeof(RequestHandlers).GetMethod($"OnRequest2");
            var statichandler = typeof(Handlers).GetMethod("OnRequest2");

            RequestHandlers hander = new RequestHandlers();
            thandler.Invoke(hander, new object[] { JsonConvert.DeserializeObject(tr.Data, t) });
        
            object ob = JsonConvert.DeserializeObject(tr.Data, t2, new JsonSerializerSettings {DefaultValueHandling = DefaultValueHandling.Include });
            thandler2.Invoke(hander, new object[] { ob });
            statichandler.Invoke(null, new object[] { ob });
            //Request1 rc1 =  as Request1;
            //Request1 rc1 = Activator.CreateInstance(t) as Request1;

            // Console.WriteLine($"{rc1.Id} {rc1.Name}");
            Console.WriteLine("Hello World!");
        }
    }
}
