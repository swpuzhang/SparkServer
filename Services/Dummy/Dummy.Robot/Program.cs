using Commons.Extenssions.Defines;
using System;
using System.Threading.Tasks;

namespace Dummy.Robot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Robot robot = new Robot(0, "facebookzhangyang1", "facebookzhangyang1", 0, "www.baidu.com");
            await robot.LoginAsync("http://localhost:5000");
            //匹配
            var matchingResponse = await robot.MatchGameAsync();
            if (matchingResponse.StatusCode != StatusCodeDefines.Success)
            {
                Console.WriteLine($"code:{matchingResponse.StatusCode} {matchingResponse.ErrorInfos}");
            }
            Console.WriteLine($"room:{matchingResponse.Body.RoomId}");
            Console.ReadLine();
        }
    }
}
