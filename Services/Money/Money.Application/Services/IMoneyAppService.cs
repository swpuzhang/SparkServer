using Money.Application.ViewModels;
using Money.Domain.Models;
using Commons.Domain.Models;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Money.Application.Services
{
    public interface IMoneyAppService
    {
        Task<HasBodyResponse<MoneyResponse>> Login(MoneyVM moneyVM);
        MoneyVM GetById(Int64 id);
      

    }
}
