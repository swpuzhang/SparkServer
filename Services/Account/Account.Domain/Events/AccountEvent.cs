using Account.Domain.Models;
using Commons.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Events
{
    public class LoginEvent : Event
    {
        public LoginEvent()
        {
        }

        public LoginEvent(Guid gid, AccountResponse accounResponse, bool isRegister, bool isNeedUpdate, AccountInfo info)
        {
            AggregateId = gid;
            AccounResponse = accounResponse;
            IsRegister = isRegister;
            IsNeedUpdate = isNeedUpdate;
            Info = info;
        }

        public AccountResponse AccounResponse { get; set; }
        public bool IsRegister { get; set; }

        public bool IsNeedUpdate { get; private set; }
        public AccountInfo Info { get; private set; }
    }

    public class FinishRegisterRewardEvent : Event
    {
        public long Id { get; private set; }
        public FinishRegisterRewardEvent(long id)
        {
            Id = id;
        }
    }
}
