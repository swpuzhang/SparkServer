using Commons.Domain.Commands;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using Commons.Domain.Models;
using Sample.Domain.Models;

namespace Sample.Domain.Commands
{
    //public class LoginCommands : Command<HasBodyResponse<SampleResponse>
    public class SampleCommand : Command<HasBodyResponse<SampleInfo>>
    {
        public SampleInfo Info { get; private set; }
        public SampleCommand(SampleInfo info)
        {
            Info = info;
        }
    }
     
}
