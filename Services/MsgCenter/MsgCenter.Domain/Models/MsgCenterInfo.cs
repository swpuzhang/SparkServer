using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Commons.MqCommands;

namespace MsgCenter.Domain.Models
{

    public class UserMsgs
    {
        public UserMsgs(List<UserMsgInfo> msgs)
        {
            Msgs = msgs;
        }

        public List<UserMsgInfo> Msgs { get; set; }
    }

}
