
using Commons.IntegrationBus.MqEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.MqEvents
{
    public class LoginMqEvent : MqEvent
    {
        public LoginMqEvent()
        {

        }
        public LoginMqEvent(long id, string platformAccount, 
            string userName, int sex, string headUrl, 
            string token, long actionTime)
        {
            Id = id;
            PlatformAccount = platformAccount;
            UserName = userName;
            Sex = sex;
            HeadUrl = headUrl;
            Token = token;
            ActionTime = actionTime;
            AggregateId = Guid.NewGuid();
        }

        public Int64 Id { get; set; }
        public string PlatformAccount { get; set; }
        public string UserName { get; set; }
        public int Sex { get; set; }
        public string HeadUrl { get; set; }
        public string Token { get; set; }

        public long ActionTime { get; set; }
    }

    public class RegistMqEvent : LoginMqEvent
    {
        public RegistMqEvent()
        {

        }
        public RegistMqEvent(long id, string platformAccount,
            string userName, int sex, string headUrl,
            string token, long actionTime)
            
        {
            Id = id;
            PlatformAccount = platformAccount;
            UserName = userName;
            Sex = sex;
            HeadUrl = headUrl;
            Token = token;
            ActionTime = actionTime;
            AggregateId = Guid.NewGuid();
        }
    }

}
