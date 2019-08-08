using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSGateWay.Manager
{
    public class UserConnManager
    {
        private Dictionary<long, string> _userConnIds = new Dictionary<long, string>();
        private Dictionary<string, long> _connUserIds = new Dictionary<string, long>();
        public object _sync = new object();

        public int GetUserCount()
        {
            lock (_sync)
            {
                return _userConnIds.Count;
            }
        }

        public long GetUidByConn(string conn)
        {
            lock (_sync)
            {
                if (!_connUserIds.TryGetValue(conn, out var id))
                {
                    return 0;
                }
                return id;
            }
        }

        public string GetConnByUid(long uid)
        {
            string conn = null;
            lock (_sync)
            {
                if (!_userConnIds.TryGetValue(uid, out conn))
                {
                    Log.Logger.Error($"some thing wrong GetConnByUid user:{uid} can't find conn");
                    return null;
                }
            }

            Log.Logger.Information($"GetConnByUid user:{uid} success conn:{conn}");
            return conn;
        }

        public bool OnLogined(long userid, string conn)
        {
            lock (_sync)
            {
                if(!_userConnIds.TryAdd(userid, conn))
                {
                    Log.Logger.Error($"some thing wrong add user:{userid} conn:{conn} user has already logined");
                    return false;
                }
                if (!_connUserIds.TryAdd(conn, userid))
                {
                    Log.Logger.Error($"some thing wrong add user:{userid} conn:{conn} user has already logined");
                    return false;
                }
            
            }
            Log.Logger.Information($"add user:{userid} in connections success conn:{conn}");
            return true;
        }

        public void OnDisconnected(string conn)
        {
            long uid = 0;
            lock (_sync)
            {
                if (!_connUserIds.TryGetValue(conn, out uid))
                {
                    Log.Logger.Error($"some thing wrong get user  conn:{conn}  error");
                    return;
                }
                _connUserIds.Remove(conn);
                if (!_userConnIds.Remove(uid))
                {
                    Log.Logger.Error($"some thing wrong remove user:{uid} conn:{conn} error");
                    return;
                }
                Log.Logger.Information($"remove user:{uid} from connections conn:{conn}  success");
            }
            
        }
    }
}
