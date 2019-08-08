using System;
using System.Collections.Generic;
using System.Text;

namespace Sangong.Domain.Models
{
    public class RoomInfo : IComparable<RoomInfo>
    {
        public const int MAX_USER_NUM = 7;
        public RoomInfo(string roomId, int userCount, string gameKey, long blind)
        {
            RoomId = roomId;
            UserCount = userCount;
            GameKey = gameKey;
            Blind = blind;
        }
        public void UpdateUserCount(int count)
        {
            UserCount = count;
        }
        public void AddUserCount(int addCount)
        {
            UserCount += addCount;
        }
        public string RoomId { get; private set; }

        public int UserCount { get; private set; }

        public string GameKey { get; private set; }

        public long Blind { get; private set; }

        public bool IsFull() => UserCount == MAX_USER_NUM;
      
        public bool IsEmpty() => UserCount == 0;
     
        public int CompareTo(RoomInfo other)
        {
            if (UserCount == other.UserCount)
            {
                return RoomId.CompareTo(other.RoomId);
            }
            return -UserCount.CompareTo(other.UserCount);
           
        }
    }
}
