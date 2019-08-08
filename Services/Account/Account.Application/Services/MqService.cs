using Account.Domain.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.Services
{
    public class MqService : IMqService
    {

        private readonly WSHostManager _hostManager;

        public MqService(WSHostManager hostManager)
        {
            _hostManager = hostManager;
        }

        public void NotifyHostInfo(string host, int userCount)
        {
            _hostManager.OnNotifyHostInfo(host, userCount);
        }
    }
}
