using Commons.Domain.RepositoryInterface;
using MsgCenter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MsgCenter.Domain.RepositoryInterface
{
    public interface IMsgCenterRedisRepository : IRedisRepository
    {
        Task<List<string>> GetAllMsgIds(long id, MsgTypes msgType);
        Task<UserMsgInfo> GetMsgInfo(long id, MsgTypes msgType, string msgId);
        Task<Dictionary<string, UserMsgInfo>> GetAllMsg(long id, MsgTypes msgType);
        Task DeleteExpiredMsgs(long id, MsgTypes msgType);
        Task DeleteMsg(long id, MsgTypes msgType, string msgiD);
        Task ReadedMsg(long id, MsgTypes msgType, string MsgId);
        Task AddMsgInfo(long id, MsgTypes msgType, UserMsgInfo info);
        Task SetMsgInfo(long id, MsgTypes msgType, UserMsgInfo info);
        Task ReadedAllMsg(long id, MsgTypes msgType);

    }
}
