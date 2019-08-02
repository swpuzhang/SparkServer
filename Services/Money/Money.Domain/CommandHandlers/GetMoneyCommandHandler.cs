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
    public class GetMoneyCommandHandler :
        IRequestHandler<GetMoneyCommand, BodyResponse<MoneyInfo>>
    {
        private readonly IMoneyInfoRepository _moneyRepository;
        private readonly IMoneyRedisRepository _redis;
        public GetMoneyCommandHandler(IMoneyInfoRepository rep,  RedisHelper redis, IMediatorHandler bus,
            IMoneyRedisRepository moneyRedis)
        {
            _moneyRepository = rep;
            _redis = moneyRedis;
        }
        public async Task<BodyResponse<MoneyInfo>> Handle(GetMoneyCommand request, CancellationToken cancellationToken)
        {

            var moneyInfo = await _redis.GetMoney(request.Id);
            if (moneyInfo == null)
            {
                using (var locker = _redis.Loker(KeyGenHelper.GenUserKey(request.Id, MoneyInfo.ClassName)))
                {
                    locker.Lock();
                    moneyInfo = await _moneyRepository.FindAndAdd(request.Id, new MoneyInfo(request.Id, 0, 0, 0, 0));
                    _ = _redis.SetMoney(request.Id, moneyInfo);
                }
                
                if (moneyInfo == null)
                {
                    return new BodyResponse<MoneyInfo>(StatuCodeDefines.GetMoneyError, null, null);
                }
                
            }
            BodyResponse<MoneyInfo> response = new BodyResponse<MoneyInfo>(StatuCodeDefines.Success, null, moneyInfo);
            return response;

        }
    }
}
