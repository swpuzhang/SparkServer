using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WSGateWay.Manager
{

    

    public class RoomIdMapGameManager
    {
        public  void AddMap(List<KeyValuePair<string, string>> pairs)
        {
            pairs.ForEach(t =>
            {
                _maps.AddOrUpdate(t.Key, t.Value, (key, value) => t.Value);
            });
        }
        private ConcurrentDictionary<string, string> _maps = new ConcurrentDictionary<string, string>();

        public string GetGameByRoomid(string roomid)
        {
            if (_maps.TryGetValue(roomid, out var value))
            {
                return value;
            }
            return "";
        }
    }
}
