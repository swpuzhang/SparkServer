﻿using System;
using System.IO;
using Newtonsoft.Json;

namespace JsonTest
{
    public class Person
    {
       
        public string Id { get; set; }
        /*public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }*/
    }

    public class AccountInfo
    {
        public long Id { get; private set; }
        public string PlatformAccount { get; private set; }

        public string UserName { get; private set; }

        public int Sex { get; private set; }

        public string HeadUrl { get; private set; }

        public int Type { get; set; }

        public AccountInfo()
        {

        }

        [JsonConstructor]
        public AccountInfo(long id, string platformAccount, string userName, int sex, string headUrl, int type)
        {
            Id = id;
            PlatformAccount = platformAccount;
            UserName = userName;
            Sex = sex;
            HeadUrl = headUrl;
            Type = type;
        }


        public override bool Equals(object obj)
        {
            return obj is AccountInfo info &&
                   PlatformAccount == info.PlatformAccount &&
                   UserName == info.UserName &&
                   Sex == info.Sex &&
                   HeadUrl == info.HeadUrl &&
                   Type == info.Type;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            string home = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string basePath = Path.Combine(home, "work/SwaggerInterface");
            Console.WriteLine(basePath);
            DirectoryInfo folder = new DirectoryInfo(basePath);
            foreach (FileInfo file in folder.GetFiles())
            {
                Console.WriteLine(file.FullName);
            }
            /*string str = "{\"PlatformAccount\":\"facebookzhangyang\",\"UserName\":\"zhangyang\",\"Sex\":0,\"HeadUrl\":\"www.baidu.com\",\"Type\":0,\"Id\":10000000002}";
            var account = JsonConvert.DeserializeObject<AccountInfo>(str);
            Person p = new Person() { Id = "1" };//, Age = 20, Id = 1, Name = "zhangyang" };
            BsonClassMap.RegisterClassMap<Person>(x =>
            {
                x.MapIdProperty(c => c.Id);
            });
            var bson = BsonDocument.Create((object)p);

            Console.WriteLine("111");*/
            /**/
            /*Person p = new Person()
            {
                Id = 1,
                Name = "zhangyang",
                Age = 28,
               
            };
            string str = JsonConvert.SerializeObject(p);
            Console.WriteLine(str);

            string json = @"{
  'FullName': 'Dan Deleted',
  'Deleted': true,
  'DeletedDate': '2013-01-20T00:00:00'
}";
            try
            {
                Person pd = JsonConvert.DeserializeObject<Person>(json);
            }
            catch(JsonSerializationException ex)
            {
                Console.WriteLine(ex.Message);
            }


            try
            {
                Person pd = JsonConvert.DeserializeObject<Person>(json, new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error });
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine(ex.Message);
            }*/

        }
    }
}
