using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyNetQTest
{
    public interface IService
    {
        void SomeService();
    }

    public class Service : IService
    {
        public void SomeService()
        {
            Console.WriteLine("SomeService");
        }
    }
}
