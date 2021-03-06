﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.MqEvents
{
    public class HostInfoEvent
    {
        public HostInfoEvent(string host, int userCount)
        {
            Host = host;
            UserCount = userCount;
        }

        public string Host { get; private set; }
        public int UserCount { get; private set; }
    }
}
