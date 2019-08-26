using Account.Application.ViewModels;
using Account.Domain.Models;
using Commons.Domain.Models;
using Commons.Infrastruct;
using Commons.MqCommands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Services
{
    public interface IAccountAppService
    {
        Task<BodyResponse<AccountResponseVM>> Login(AccountVM StudentViewModel);
        Task<BodyResponse<AccountDetailVM>> GetSelfAccount(long id);

        Task<BodyResponse<GetAccountBaseInfoMqResponse>> GetAccountBaseInfo(long id);
        void FinishRegisterReward(long id);
        Task<BodyResponse<GetIdByPlatformMqResponse>> GetIdByPlatform(string platformAccount, int type);
        Task<BodyResponse<OtherAccountDetaiVM>> GetOtherAccount(long id, long otherId);
    }
}
