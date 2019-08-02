using System;
using System.Collections.Generic;
using System.Text;

namespace Sangong.Domain.Models
{
    public class RoomInfo : IComparable<RoomInfo>
    {
        const int MAX_USER_NUM = 7;
        public RoomInfo(int roomId, int userCount, string gameKey)
        {
            RoomId = roomId;
            UserCount = userCount;
            GameKey = gameKey;
        }

        public int RoomId { get; private set; }

        public int UserCount { get; private set; }

        public string GameKey { get; private set; }

        public bool IsFull() => UserCount == MAX_USER_NUM;
      
        public bool IsEmpty() => UserCount == 0;
     
        public int CompareTo(RoomInfo other)
        {
            if (UserCount == other.UserCount)
            {
                if (RoomId < other.RoomId)
                {
                    return 1;
                }
                else if (RoomId > other.RoomId)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            if (UserCount > other.UserCount)
            {
                return -1;
            }
            return 1;
        }
    }
}
