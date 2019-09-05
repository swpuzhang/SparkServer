using Commons.Domain.Commands;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using Commons.Domain.Models;
using AppSetting.Domain.Models;

namespace AppSetting.Domain.Commands
{
    //public class LoginCommands : Command<BodyResponse<AppSettingResponse>
    public class FacebackCommand : Command<BodyResponse<string>>
    {
        public long Id { get; private set; }
        public UserFadeback Fadeback { get; private set; }
        public FacebackCommand(long id, UserFadeback fadeback)
        {
            Id = id;
            Fadeback = fadeback;
        }
    }
     
    public class GetUnreadReplyCommand : Command<BodyResponse<FadebackReplyNum>>
    {
        public long Id { get; private set; }
        public GetUnreadReplyCommand(long id)
        {
            Id = id;
        }
    }
}
