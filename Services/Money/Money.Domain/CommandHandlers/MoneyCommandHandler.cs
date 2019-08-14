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
using Serilog;
using MassTransit;
using Commons.Message.MqEvents;

namespace Money.Domain.CommandHandlers
{
    public class GetMoneyCommandHandler :
        IRequestHandler<GetMoneyCommand, BodyResponse<MoneyInfo>>,
        IRequestHandler<BuyInCommand, BodyResponse<MoneyMqResponse>>,
        IRequestHandler<AddMoneyCommand, BodyResponse<MoneyMqResponse>>
    {
        private readonly IMoneyInfoRepository _moneyRepository;
        private readonly IMoneyRedisRepository _redis;
        private readonly IBusControl _busCtl;
        public GetMoneyCommandHandler(IMoneyInfoRepository rep,
            RedisHelper redis, IMediatorHandler bus,
            IMoneyRedisRepository moneyRedis, IBusControl busCtl)
        {
            _moneyRepository = rep;
            _redis = moneyRedis;
            _busCtl = busCtl;
        }
        public async Task<BodyResponse<MoneyInfo>> Handle(GetMoneyCommand request,
            CancellationToken cancellationToken)
        {

            var moneyInfo = await _redis.GetMoney(request.Id);
            if (moneyInfo == null)
            {
                using (var locker = _redis.Loker(KeyGenHelper.GenUserKey(request.Id, 
                    MoneyInfo.ClassName)))
                {
                    await locker.LockAsync();
                    moneyInfo = await _moneyRepository.FindAndAdd(request.Id, 
                        new MoneyInfo(request.Id, 0, 0, 0, 0, 0));
                    _ = _redis.SetMoney(request.Id, moneyInfo);
                }

                if (moneyInfo == null)
                {
                    return new BodyResponse<MoneyInfo>(StatuCodeDefines.GetMoneyError, null, null);
                }

            }
            BodyResponse<MoneyInfo> response = new BodyResponse<MoneyInfo>
                (StatuCodeDefines.Success, null, moneyInfo);
            Log.Debug($"GetMoneyCommand:{moneyInfo.CurCoins},{moneyInfo.Carry}");
            return response;

        }

        public async Task<BodyResponse<MoneyMqResponse>> Handle(BuyInCommand request, 
            CancellationToken cancellationToken)
        {
           
            using (var locker = _redis.Loker(KeyGenHelper.GenUserKey(request.Id,
                MoneyInfo.ClassName)))
            {
                await locker.LockAsync();
                var moneyInfo = await _redis.GetMoney(request.Id);
                if (moneyInfo == null)
                {
                    moneyInfo = await _moneyRepository.FindAndAdd(request.Id, 
                        new MoneyInfo(request.Id, 0, 0, 0, 0, 0));
                }
          
                if (moneyInfo.CurCoins + moneyInfo.Carry < request.MinBuy)
                {
                    return new BodyResponse<MoneyMqResponse>
                        (StatuCodeDefines.NoEnoughMoney, null, null);
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
                if (left < 0)
                {
                    moneyInfo.AddCoins(left);
                    moneyInfo.AddCarry(realBuy);
                }
                
                await Task.WhenAll(_redis.SetMoney(request.Id, moneyInfo), _moneyRepository.ReplaceAsync(moneyInfo));
                return new BodyResponse<MoneyMqResponse>(StatuCodeDefines.Success, null, 
                    new MoneyMqResponse(request.Id, moneyInfo.CurCoins, moneyInfo.CurDiamonds, 
                    moneyInfo.MaxCoins, moneyInfo.MaxDiamonds, moneyInfo.Carry));
            }
        }

        public async Task<BodyResponse<MoneyMqResponse>> Handle(AddMoneyCommand request, 
            CancellationToken cancellationToken)
        {
            
            using (var locker = _redis.Loker(KeyGenHelper.GenUserKey(request.Id,
                MoneyInfo.ClassName)))
            {
                await locker.LockAsync();
                Log.Debug($"AddMoneyCommand add begin:{request.AddCoins},{request.AddCarry} {request.AggregateId}");
                var moneyInfo = await _redis.GetMoney(request.Id);
                if (moneyInfo == null)
                {
                    moneyInfo = await _moneyRepository.FindAndAdd(request.Id, 
                        new MoneyInfo(request.Id, 0, 0, 0, 0, 0));
                }

                if (request.AddCoins < 0 && 
                    System.Math.Abs(request.AddCoins) > 
                    moneyInfo.CurCoins)
                {
                    Log.Debug($"AddMoneyCommand add end:{request.AddCoins},{request.AddCarry} {request.AggregateId}--1");
                    return new BodyResponse<MoneyMqResponse>
                        (StatuCodeDefines.NoEnoughMoney, null, null);
                }

                if (request.AddCarry < 0 && 
                    System.Math.Abs(request.AddCarry) > moneyInfo.Carry)
                {
                    Log.Debug($"AddMoneyCommand add end:{request.AddCoins},{request.AddCarry} {request.AggregateId}--2");
                    return new BodyResponse<MoneyMqResponse>
                        (StatuCodeDefines.NoEnoughMoney, null, null);
                }
                moneyInfo.AddCoins(request.AddCoins);
                moneyInfo.AddCarry(request.AddCarry);
                moneyInfo.UpdateMaxCoins();
                long coinsChangedCount = request.AddCoins + request.AddCarry;
                if (coinsChangedCount != 0)
                {
                    _ = _busCtl.Publish<MoneyChangedMqEvent>(new MoneyChangedMqEvent
                        (moneyInfo.CurCoins,
                        moneyInfo.CurDiamonds, moneyInfo.MaxCoins,
                        moneyInfo.MaxDiamonds, coinsChangedCount, 0));
                }
                await Task.WhenAll(_redis.SetMoney(request.Id, moneyInfo),
                    _moneyRepository.ReplaceAsync(moneyInfo));
                Log.Debug($"AddMoneyCommand add end:{request.AddCoins},{request.AddCarry} {request.AggregateId} curCoins:{moneyInfo.CurCoins} curCarry:{moneyInfo.Carry}--3");

                return new BodyResponse<MoneyMqResponse>(StatuCodeDefines.Success, null,
                    new MoneyMqResponse(request.Id, moneyInfo.CurCoins, moneyInfo.CurDiamonds,
                    moneyInfo.MaxCoins, moneyInfo.MaxDiamonds, moneyInfo.Carry));
            }

        }
    }
}
