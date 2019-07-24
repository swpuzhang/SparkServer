using System;
using Newtonsoft.Json;

namespace JsonTest
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
    class Program
    {

        static void Main(string[] args)
        {
            /**/
            Person p = new Person()
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
            }

        }
    }
}
