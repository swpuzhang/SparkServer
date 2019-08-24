﻿using Commons.Domain.Commands;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using Commons.Domain.Models;
using MsgCenter.Domain.Models;

namespace MsgCenter.Domain.Commands
{
    //public class LoginCommands : Command<BodyResponse<MsgCenterResponse>
    public class GetUserMsgsCommand : Command<BodyResponse<UserMsgs>>
    {
        public GetUserMsgsCommand(long id, MsgTypes msgType)
        {
            Id = id;
            MsgType = msgType;
        }

        public long Id { get; private set; }
        public MsgTypes MsgType { get; private set; }

    }

    public class ReadedCommand : Command<BodyResponse<NullBody>>
    {
        public ReadedCommand(long id, MsgTypes msgType, string msgId)
        {
            Id = id;
            MsgType = msgType;
            MsgId = msgId;
        }

        public long Id { get; private set; }
        public MsgTypes MsgType { get; private set; }
        public string MsgId { get; private set; }
    }
    public class RecieveMsgReward : Command<BodyResponse<NullBody>>
    {
        public RecieveMsgReward(long id, string msgId)
        {
            Id = id;
            MsgId = msgId;
        }

        public long Id { get; private set; }
        public string MsgId { get; private set; }
    }
    public class DeleteMsgCommand : Command<BodyResponse<NullBody>>
    {
        public DeleteMsgCommand(long id, MsgTypes msgType, string msgId)
        {
            Id = id;
            MsgType = msgType;
            MsgId = msgId;
        }

        public long Id { get; private set; }
        public MsgTypes MsgType { get; private set; }
        public string MsgId { get; private set; }

    }

    public class ReadedAllCommand : Command<BodyResponse<NullBody>>
    {
        public ReadedAllCommand(long id, MsgTypes msgType)
        {
            Id = id;
            MsgType = msgType;
        }

        public long Id { get; private set; }
        public MsgTypes MsgType { get; private set; }
    }
    public class RecieveAllMsgReward : Command<BodyResponse<List<RewardInfo>>>
    {
        public RecieveAllMsgReward(long id)
        {
            Id = id;
        }

        public long Id { get; private set; }
    }
    
}
