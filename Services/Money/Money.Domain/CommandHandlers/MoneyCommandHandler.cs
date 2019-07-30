using Money.Domain.Commands;
using Money.Domain.Models;
using Money.Domain.RepositoryInterface;
using Commons.Domain.Bus;
using Commons.Domain.CommandHandler;
using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using Commons.Extenssions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Money.Domain.CommandHandlers
{
    public class MoneyCommandHandler :
        IRequestHandler<LoginCommand, HasBodyResponse<MoneyResponse>>
    {
        //private readonly readonly IRequestClient<DoSomething> _requestClient;
    
        protected readonly IMediatorHandler _bus;
        private readonly IMoneyInfoRepository _moneyRepository;
        private readonly  IUserIdGenRepository _genRepository;
        private readonly RedisHelper _redis;
        public MoneyCommandHandler(IMoneyInfoRepository rep, IUserIdGenRepository genRepository, RedisHelper redis, IMediatorHandler bus)
        {
            _moneyRepository = rep;
            _genRepository = genRepository;
            _redis = redis;
            _bus = bus;
        }
        public Task<HasBodyResponse<MoneyResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            
            HasBodyResponse<MoneyResponse> response = new HasBodyResponse<MoneyResponse>(StatuCodeDefines.LoginError, null, null);
            return Task.FromResult(response);

        }
    }
}
