using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WSGateWay
{

    

    public class RoomIdMapGame
    {
        public  void AddMap(List<KeyValuePair<int, string>> pairs)
        {
            pairs.ForEach(t =>
            {
                _maps.AddOrUpdate(t.Key, t.Value, (key, value) => t.Value);
            });
        }
        private ConcurrentDictionary<int, string> _maps = new ConcurrentDictionary<int, string>();
    }
}
