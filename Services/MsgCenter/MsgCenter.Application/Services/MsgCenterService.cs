using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MsgCenter.Application.ViewModels;
using MsgCenter.Domain;
using MsgCenter.Domain.Commands;
using MsgCenter.Domain.Models;
using MsgCenter.Domain.RepositoryInterface;
using AutoMapper;
using Commons.Domain.Bus;
using Commons.Domain.Models;
using Commons.Infrastruct;
using Commons.MqCommands;

namespace MsgCenter.Application.Services
{
    public class MsgCenterService : IMsgCenterService
    {
        private readonly IMediatorHandler _bus;
        public MsgCenterService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public Task<BodyResponse<NullBody>> DeleteMsg(long id, MsgTypes msgType, string msgId)
        {
            return _bus.SendCommand(new DeleteMsgCommand(id, msgType, msgId));
        }

        public Task<BodyResponse<UserMsgs>> GetUserMsgs(long id, MsgTypes msgType)
        {
            return  _bus.SendCommand(new GetUserMsgsCommand(id, msgType));
        }

        public Task<BodyResponse<NullBody>> ReadedMsg(long id, MsgTypes msgType, string msgId)
        {
            return _bus.SendCommand(new ReadedCommand(id, msgType, msgId));
        }
        public Task<BodyResponse<NullBody>> RecieveMsgReward(long id, string msgId)
        {
            return _bus.SendCommand(new RecieveMsgReward(id, msgId));
        }

        public Task<BodyResponse<NullBody>> ReadedAllMsg(long id, MsgTypes msgType)
        {
            return _bus.SendCommand(new ReadedAllCommand(id, msgType));
        }

        public Task<BodyResponse<List<RewardInfo>>> RecieveAllMsgReward(long id)
        {
            return _bus.SendCommand(new RecieveAllMsgRewardCommand(id));
        }

        public Task<BodyResponse<NullBody>> PushMsg(long id, UserMsgInfo msg)
        {
            return _bus.SendCommand(new PushMsgCommand(id, msg));
        }
    }
}
