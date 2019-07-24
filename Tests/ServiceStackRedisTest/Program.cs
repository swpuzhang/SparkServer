using System;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System.Linq;
using StackExchange.Redis;

namespace ServiceStackRedisTest
{
    public class Car
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class Books
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var redisManager = new RedisManagerPool("localhost:6379");
            using (var redisClient = redisManager.GetClient())
            {
                
                var cars = redisClient.As<Car>();
                var onecar= new Car
                {
                    Id = 100,
                    Title = "test",
                    Description = "test"
                };
                
                cars.Store(onecar, TimeSpan.FromSeconds(5));
                
                var allcars = cars.GetAll();
                foreach (var c in allcars)
                {
                    Console.WriteLine($"Redis Has a ->{c.Id} {c.Title} {c.Description}");
                }
                redisClient.Set<Car>("car", onecar);
                var books = redisClient.As<Books>();
                var onebook = new Books
                {
                    Id = 1,
                    Title = "test",
                    Description = "test"
                };
                books.Store(onebook);
            }


            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
            var db = redis.GetDatabase();
           
            Console.WriteLine("Hello World!");
        }
    }
}
