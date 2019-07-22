using Account.Application.ViewModels;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.Services
{
    public interface IAccountAppService
    {
        HasBodyResponseVM<AccounResponsetVM> Login(AccountVM StudentViewModel);
        AccountVM GetById(Int64 id);
        void Update(AccountVM StudentViewModel);
      

    }
}
