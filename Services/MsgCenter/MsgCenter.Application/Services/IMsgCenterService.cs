using MsgCenter.Application.ViewModels;
using MsgCenter.Domain.Models;
using Commons.Domain.Models;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MsgCenter.Application.Services
{
    public interface IMsgCenterService
    {
        Task<BodyResponse<UserMsgs>> GetUserMsgs(long id, MsgTypes msgType);
        Task<BodyResponse<NullBody>> ReadedMsg(long id, MsgTypes msgType, string msgId);
        Task<BodyResponse<NullBody>> RecieveMsgReward(long id, string msgId);
        Task<BodyResponse<NullBody>> DeleteMsg(long id, MsgTypes msgType, string msgId);
        Task<BodyResponse<NullBody>> ReadedAllMsg(long id, MsgTypes msgType);
        Task<BodyResponse<List<RewardInfo>>> RecieveAllMsgReward(long id);
    }
}
