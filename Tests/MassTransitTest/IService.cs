using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MassTransitTest
{
    public interface IService : IDisposable
    {
        void SomeService();
    }

    public interface IService2 : IDisposable
    {
        void SomeService();
    }

    public class Service : IService
    {
        IService2 _service2;

        public Service(IService2 service2)
        {
            _service2 = service2;
        }

        public void Dispose()
        {
            Console.WriteLine("SomeService dispose");
        }

        public void SomeService()
        {
            Console.WriteLine("SomeService");
            _service2.SomeService();
        }
    }

    public class Service2 : IService2
    {
        public void Dispose()
        {
            Console.WriteLine("SomeService222 dispose");
        }

        public void SomeService()
        {
            Console.WriteLine("SomeService222");
        }
    }
}
