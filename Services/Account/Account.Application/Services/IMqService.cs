using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.Services
{
    public interface IMqService
    {
        void NotifyHostInfo(string host, int userCount);
    }
}
