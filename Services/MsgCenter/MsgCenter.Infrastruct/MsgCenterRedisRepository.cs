using Commons.Domain.RepositoryInterface;
using Commons.Extenssions;
using Commons.MqCommands;
using MsgCenter.Domain.Models;
using MsgCenter.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MsgCenter.Infrastruct
{
    public class MsgCenterRedisRepository : RedisRepository, IMsgCenterRedisRepository
    {

        public MsgCenterRedisRepository(RedisHelper redis) : base(redis)
        {

        }
        public Task<List<string>> GetAllMsgIds(long id, MsgTypes msgType)
        {
             return _redis.GetZsetAllKeyAsync(KeyGenHelper.GenUserKey(id, "MsgIds", msgType.ToString()));
        }
        public Task<UserMsgInfo> GetMsgInfo(long id,  MsgTypes msgType, string msgId)
        {
            return _redis.GetHashValueAsync<UserMsgInfo>(KeyGenHelper.GenUserKey(id, "Msgs", msgType.ToString()), msgId);
        }

        public Task AddMsgInfo(long id, MsgTypes msgType, UserMsgInfo info)
        {
            return Task.WhenAll(_redis.AddZsetValueAsync(KeyGenHelper.GenUserKey(id, "MsgIds", msgType.ToString()),
                info.MsgId, DateTime.Now.ToTimeStamp(), TimeSpan.FromDays(30)),
                _redis.AddHashValueAsync(KeyGenHelper.GenUserKey(id, "Msgs", msgType.ToString()), 
                info.MsgId, info, TimeSpan.FromDays(30)), DeleteExpiredMsgs(id, msgType));
        }

        public Task SetMsgInfo(long id, MsgTypes msgType, UserMsgInfo info)
        {
            return _redis.AddHashValueAsync(KeyGenHelper.GenUserKey(id, "Msgs", msgType.ToString()), info.MsgId, info);
        }

        public Task<Dictionary<string, UserMsgInfo>> GetAllMsg(long id, MsgTypes msgType)
        {
            return _redis.GetHashAllAsync<string, UserMsgInfo>(KeyGenHelper.GenUserKey(id, "Msgs", msgType.ToString()));
        }

        public async Task DeleteExpiredMsgs(long id, MsgTypes msgType)
        {
            string key = KeyGenHelper.GenUserKey(id, "MsgIds", msgType.ToString());
            var deletedKeys = await _redis.DeleteZsetReturnValueRangeAsync
                (key, 0,(DateTime.Now - TimeSpan.FromDays(30)).ToTimeStamp());
            await _redis.DeleteHashValuesAsync(KeyGenHelper.GenUserKey(id, "Msgs", msgType.ToString()), deletedKeys);
        }

        public async Task DeleteMsg(long id, MsgTypes msgType, string msgId)
        {
          
            await _redis.DeleteHashValueAsync(KeyGenHelper.GenUserKey(id, "Msgs", msgType.ToString()), msgId);
            await _redis.DeleteZsetValueAsync(KeyGenHelper.GenUserKey(id, "MsgIds", msgType.ToString()), msgId);
        }

        public async Task ReadedMsg(long id, MsgTypes msgType, string msgId)
        {
            var msginfo = await GetMsgInfo(id, msgType, msgId);
            if (msginfo != null && msginfo.MsgState == MsgStates.Unread)
            {
                msginfo.MsgState = MsgStates.Readed;
                await SetMsgInfo(id, msgType, msginfo);
            }
        }

        public async Task ReadedAllMsg(long id, MsgTypes msgType)
        {
            var msginfos = await GetAllMsg(id, msgType);

            List<Task> tasks = new List<Task>();
            foreach (var one in msginfos)
            {
                if (one.Value.MsgState == MsgStates.Unread)
                {
                    one.Value.MsgState = MsgStates.Readed;
                }
                tasks.Add(SetMsgInfo(id, msgType, one.Value));
            }
            await Task.WhenAll(tasks);
        }
    }
}
