using Commons.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSetting.Domain.Events
{
    public class AppSettingEvent : Event
    {
        public AppSettingEvent()
        {
        }

        public AppSettingEvent(Guid gid)
        {
            AggregateId = gid;
        }

       
    }
}
