using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionTest
{
    public class RoomInfo : IComparable<RoomInfo>
    {
        public int RoomId { get; set; }
        public int Count { get; set; }

        public int CompareTo(RoomInfo other)
        {
            if (Count == other.Count)
            {
                if (RoomId < other.RoomId)
                {
                    return -1;
                }
                else if (RoomId > other.RoomId)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            if (Count < other.Count)
            {
                return -1;
            }
            return 1;
        }
    }



    class Program
    {
        static void ThrowException()
        {
            try
            {
                throw new Exception("test");
            }
            
            finally
            {
                Console.WriteLine("finally");
            }
        }

        static async Task LinqTask()
        {
            var ret = Enumerable.Range(0, 2).Select(x =>
                {
                    Console.WriteLine(x);
                    return x;
                });
            
            var ts1 = Enumerable.Range(0, 2).Select(i =>
               Task.Run(async () =>
               {
                   Console.WriteLine($"Task {i} is running");
                   await Task.Delay(10);
                   return i;
               }));
            await Task.WhenAll(ts1);
            await Task.WhenAll(ts1);
            await Task.WhenAll(ts1);
        }

        static async Task TestTaskException()
        {
            var ts1 = Enumerable.Range(0, 10).Select(i =>
                Task.Run(async () =>
                {
                    Console.WriteLine($"Task {i} is running");
                    if (i == 5)
                    {
                        throw new Exception($"Exit at {i}");
                    }
                    if (i == 10)
                    {
                        throw new Exception($"Exit at {i}");
                    }
                    await Task.Delay(10);
                    return i;
                }));



            List<Task<int>> ts2 = new List<Task<int>>();
            for (int i = 0; i < 10; ++i)
            {
                int itemp = i;
                var t = Task.Run(async () =>
               {
                    
                   Console.WriteLine($"Task {itemp} is running");
                   if (itemp == 5)
                   {
                       throw new Exception($"Exit at {itemp}");
                   }
                   if (itemp == 10)
                   {
                       throw new Exception($"Exit at {itemp}");
                   }
                   await Task.Delay(10);
                   return itemp;
               });
                ts2.Add(t);
            }

            try
            {
                await Task.WhenAll(ts2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            try
            {
                await Task.WhenAll(ts2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            foreach (var oneTask in ts2)
            {
                if (oneTask.IsCompletedSuccessfully)
                {
                    Console.WriteLine(oneTask.Result);
                }
                
            }
        }
        static async Task Main(string[] args)
        {
            await LinqTask();
            try
            {
                await TestTaskException();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            SortedSet<RoomInfo> sset = new SortedSet<RoomInfo>()
            {
                new RoomInfo{ RoomId = 10, Count = 1 },
                new RoomInfo{ RoomId = 8, Count = 4 },
                new RoomInfo{ RoomId = 9, Count = 3 },
                new RoomInfo{ RoomId = 7, Count = 6 },
                new RoomInfo{ RoomId = 2, Count = 1 },
            };
            foreach (var one in sset)
            {
                Console.WriteLine($"{one.RoomId},{one.Count}");
            }

            try
            {
                ThrowException();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("finally");
            }
        }
    }
}
