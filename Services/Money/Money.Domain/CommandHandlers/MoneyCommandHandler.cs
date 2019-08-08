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
using Commons.MqCommands;

namespace Money.Domain.CommandHandlers
{
    public class GetMoneyCommandHandler :
        IRequestHandler<GetMoneyCommand, BodyResponse<MoneyInfo>>,
        IRequestHandler<BuyInCommand, BodyResponse<MoneyMqResponse>>,
        IRequestHandler<AddMoneyCommand, BodyResponse<MoneyMqResponse>>
    {
        private readonly IMoneyInfoRepository _moneyRepository;
        private readonly IMoneyRedisRepository _redis;
        public GetMoneyCommandHandler(IMoneyInfoRepository rep, RedisHelper redis, IMediatorHandler bus,
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
                    moneyInfo = await _moneyRepository.FindAndAdd(request.Id, new MoneyInfo(request.Id, 0, 0, 0, 0, 0));
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

        public async Task<BodyResponse<MoneyMqResponse>> Handle(BuyInCommand request, CancellationToken cancellationToken)
        {
            using (var locker = _redis.Loker(KeyGenHelper.GenUserKey(request.Id, MoneyInfo.ClassName)))
            {
                await locker.LockAsync();
                var moneyInfo = await _redis.GetMoney(request.Id);
                if (moneyInfo == null)
                {
                    moneyInfo = await _moneyRepository.FindAndAdd(request.Id, new MoneyInfo(request.Id, 0, 0, 0, 0, 0));
                }
          
                if (moneyInfo.CurCoins + moneyInfo.Carry < request.MinBuy)
                {
                    return new BodyResponse<MoneyMqResponse>(StatuCodeDefines.NoEnoughMoney, null, null);
                }
                long realBuy = 0;
                if (moneyInfo.CurCoins + moneyInfo.Carry >= request.MaxBuy)
                {
                    realBuy = request.MaxBuy;
                }
                else
                {
                    realBuy = moneyInfo.CurCoins + moneyInfo.Carry;
                }
                long left = moneyInfo.Carry - realBuy;
                if (left >= 0)
                {
                    moneyInfo.Carry = left;
                }
                else
                {
                    moneyInfo.Carry = 0;
                    moneyInfo.CurCoins += left;
                }
                _ = _redis.SetMoney(request.Id, moneyInfo);
                _ = _moneyRepository.ReplaceAsync(moneyInfo);
                return new BodyResponse<MoneyMqResponse>(StatuCodeDefines.Success, null, 
                    new MoneyMqResponse(request.Id, moneyInfo.CurCoins, moneyInfo.CurDiamonds, 
                    moneyInfo.MaxChips, moneyInfo.MaxDiamonds, moneyInfo.Carry));
            }
        }

        public async Task<BodyResponse<MoneyMqResponse>> Handle(AddMoneyCommand request, CancellationToken cancellationToken)
        {
            using (var locker = _redis.Loker(KeyGenHelper.GenUserKey(request.Id, MoneyInfo.ClassName)))
            {
                await locker.LockAsync();
                var moneyInfo = await _redis.GetMoney(request.Id);
                if (moneyInfo == null)
                {
                    moneyInfo = await _moneyRepository.FindAndAdd(request.Id, new MoneyInfo(request.Id, 0, 0, 0, 0, 0));
                }

                if (request.AddCoins < 0 && System.Math.Abs(request.AddCoins) > moneyInfo.CurCoins)
                {
                    return new BodyResponse<MoneyMqResponse>(StatuCodeDefines.NoEnoughMoney, null, null);
                }

                if (request.AddCarry < 0 && System.Math.Abs(request.AddCarry) > moneyInfo.Carry)
                {
                    return new BodyResponse<MoneyMqResponse>(StatuCodeDefines.NoEnoughMoney, null, null);
                }
                moneyInfo.CurCoins += request.AddCoins;
                moneyInfo.Carry += request.AddCarry;
                _ = _redis.SetMoney(request.Id, moneyInfo);
                _ = _moneyRepository.ReplaceAsync(moneyInfo);
                return new BodyResponse<MoneyMqResponse>(StatuCodeDefines.Success, null,
                    new MoneyMqResponse(request.Id, moneyInfo.CurCoins, moneyInfo.CurDiamonds,
                    moneyInfo.MaxChips, moneyInfo.MaxDiamonds, moneyInfo.Carry));
            }
        }
    }
}
