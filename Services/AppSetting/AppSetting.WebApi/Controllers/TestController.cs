using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons.Infrastruct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace AppSetting.WebApi.Controllers
{

    public class TestInfo 
    {
        [BsonId]
        public Int64 Id { get; set; }
        public string PlatformAppSetting { get; set; }

        public string UserName { get; set; }

        public int Sex { get; private set; }

        public string HeadUrl { get; set; }

        public TestInfo()
        {

        }

       
        public TestInfo(int id, string appsetting, string name, int sex, string head)
        {
            Id = id;
            PlatformAppSetting = appsetting;
            UserName = name;
            Sex = sex;
            HeadUrl = head;
        }
    }

    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }

        [BsonElement("Name")]
        public string BookName { get; private set; }

        public decimal Price { get; private set; }

        public string Category { get; private set; }

        public string Author { get; private set; }
    } 


    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        IMongoSettings _settings;
        IMongoCollection<Book> _books;
        IMongoCollection<TestInfo> _infos;
        public TestController(IMongoSettings settings)
        {
            _settings = settings;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _books = database.GetCollection<Book>("Books");
            _infos = database.GetCollection<TestInfo>("AppSettingInfo");
        }

        [HttpGet]
        public List<Book> CollectionTest()
        {
            return _books.Find(x => true).ToList();
        }

        [HttpGet]
        public List<TestInfo> CollectionInfoTest()
        {
            return _infos.Find(x => true).ToList();
        }
    }
}