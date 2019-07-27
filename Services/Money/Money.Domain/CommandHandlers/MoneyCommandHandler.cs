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
        public async Task<HasBodyResponse<MoneyResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var newMoneyInfo = request.Info;
            //根据PlatformMoney字段读取redis，判断该账号是否已经注册
            var loginCheckInfo = await _redis.GetStringAsync<LoginCheckInfo>(KeyGenHelper.GenKey(request.Info.PlatformMoney, typeof(LoginCheckInfo).Name));
            MoneyInfo moneyInfo = null;
            bool isRegister = false;
            if (loginCheckInfo != null)
            {
                //直接通过ID去数据库查找这个玩家信息
                moneyInfo = await _moneyRepository.GetByIdAsync(loginCheckInfo.Id);
                //生成Token, 返回MoneyResponse 发布总线事件，通知本进程和其他进程， 本进程事件更新玩家头像和状态，其他进程各自处理 
            }

            else
            {
                //查找数据库中是否有这个账号
                moneyInfo = await _moneyRepository.GetByPlatformAsync(newMoneyInfo.PlatformMoney);
                if (moneyInfo == null)
                {

                    //注册新账号
                    isRegister = true;
                    long newUid = await _genRepository.GenNewId();
                    moneyInfo = new MoneyInfo(newUid, newMoneyInfo.PlatformMoney, 
                        newMoneyInfo.UserName, newMoneyInfo.Sex, newMoneyInfo.HeadUrl, newMoneyInfo.Type);
                    await _moneyRepository.AddAsync(moneyInfo);

                    //通知注册事件

                }

            }

            if (moneyInfo != null)
            {
                //如果登录信息有更新, 那么更新数据库
                if (!isRegister && moneyInfo != newMoneyInfo)
                {
                    await _moneyRepository.UpdateAsync(moneyInfo);
                }
                //设置登录信息
                await _redis.SetStringAsync<LoginCheckInfo>(KeyGenHelper.GenKey(request.Info.PlatformMoney, typeof(LoginCheckInfo).Name), 
                    new LoginCheckInfo(moneyInfo.Id, newMoneyInfo.PlatformMoney, newMoneyInfo.Type), 
                    TimeSpan.FromDays(7));
                //设置玩家信息缓存
                return new HasBodyResponse<MoneyResponse>(StatuCodeDefines.Success, null, new MoneyResponse(moneyInfo.Id,
                    moneyInfo.PlatformMoney,
                    newMoneyInfo.UserName,
                    newMoneyInfo.Sex,
                    newMoneyInfo.HeadUrl,
                    ""));
            }

            HasBodyResponse<MoneyResponse> response = new HasBodyResponse<MoneyResponse>(StatuCodeDefines.LoginError,
                null, null);
            
            return response;

        }
    }
}
