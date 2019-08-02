using Commons.Domain.Models;
using Commons.MqCommands;
using Money.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Money.Application.Services
{
    public interface IMoneyService
    {
         
        Task<BodyResponse<GetMoneyMqResponse>> GetMoney(long id);
    }
}
