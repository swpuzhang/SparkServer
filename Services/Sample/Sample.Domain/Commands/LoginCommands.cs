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
    public class LoginCommand : Command<HasBodyResponse<SampleResponse>>
    {
        public SampleInfo Info { get; private set; }
        public LoginCommand(SampleInfo info)
        {
            Info = info;
        }
    }
     
}
