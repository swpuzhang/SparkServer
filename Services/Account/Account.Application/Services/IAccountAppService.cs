using Account.Application.ViewModels;
using Account.Domain.Models;
using Commons.Domain.Models;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Services
{
    public interface IAccountAppService
    {
        Task<HasBodyResponse<AccountResponseVM>> Login(AccountVM StudentViewModel);
        Task<HasBodyResponse<AccountDetailVM>> GetSelfAccount(long id);
    }
}
