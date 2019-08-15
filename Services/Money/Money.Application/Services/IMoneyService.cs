using Commons.Domain.Models;
using Commons.MqCommands;
using System.Threading.Tasks;

namespace Money.Application.Services
{
    public interface IMoneyService
    {
         
        Task<BodyResponse<MoneyMqResponse>> GetMoney(long id);
        Task<BodyResponse<MoneyMqResponse>> BuyIn(long id, long min, long max);

        Task<BodyResponse<MoneyMqResponse>> AddMoney(long id, long addCoins, long addCarry);
    }
}
