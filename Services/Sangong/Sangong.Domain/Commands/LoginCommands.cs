using Commons.Domain.Commands;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using Commons.Domain.Models;
using Sangong.Domain.Models;

namespace Sangong.Domain.Commands
{
    //public class LoginCommands : Command<HasBodyResponse<SangongResponse>
    public class LoginCommand : Command<HasBodyResponse<SangongResponse>>
    {
        public SangongInfo Info { get; private set; }
        public LoginCommand(SangongInfo info)
        {
            Info = info;
        }
    }
     
}
