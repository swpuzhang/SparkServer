using System;
using System.Collections.Generic;

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
        static void Main(string[] args)
        {
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
