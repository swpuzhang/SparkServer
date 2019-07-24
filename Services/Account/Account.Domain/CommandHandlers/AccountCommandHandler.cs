using Account.Domain.Commands;
using Account.Domain.Models;
using Account.Domain.RepositoryInterface;
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

namespace Account.Domain.CommandHandlers
{
    public class AccountCommandHandler :
        IRequestHandler<LoginCommand, HasBodyResponse<AccountResponse>>
    {
        //private readonly readonly IRequestClient<DoSomething> _requestClient;
    
        protected readonly IMediatorHandler _bus;
        private readonly IAccountInfoRepository _accountRepository;
        private readonly  IUserIdGenRepository _genRepository;
        private readonly RedisHelper _redis;
        public AccountCommandHandler(IAccountInfoRepository rep, IUserIdGenRepository genRepository, RedisHelper redis, IMediatorHandler bus)
        {
            _accountRepository = rep;
            _genRepository = genRepository;
            _redis = redis;
            _bus = bus;
        }
        public async Task<HasBodyResponse<AccountResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var newAccountInfo = request.Info;
            //根据PlatformAccount字段读取redis，判断该账号是否已经注册
            var loginCheckInfo = await _redis.GetStringAsync<LoginCheckInfo>(KeyGenHelper.GenKey(request.Info.PlatformAccount, typeof(LoginCheckInfo).Name));
            AccountInfo accountInfo = null;
            bool isRegister = false;
            if (loginCheckInfo != null)
            {
                //直接通过ID去数据库查找这个玩家信息
                accountInfo = await _accountRepository.GetByIdAsync(loginCheckInfo.Id);
                //生成Token, 返回AccountResponse 发布总线事件，通知本进程和其他进程， 本进程事件更新玩家头像和状态，其他进程各自处理 
            }

            else
            {
                //查找数据库中是否有这个账号
                accountInfo = await _accountRepository.GetByPlatformAsync(newAccountInfo.PlatformAccount);
                if (accountInfo == null)
                {

                    //注册新账号
                    isRegister = true;
                    long newUid = await _genRepository.GenNewId();
                    accountInfo = new AccountInfo(newUid, newAccountInfo.PlatformAccount, 
                        newAccountInfo.UserName, newAccountInfo.Sex, newAccountInfo.HeadUrl, newAccountInfo.Type);
                    await _accountRepository.AddAsync(accountInfo);

                    //通知注册事件

                }

            }

            if (accountInfo != null)
            {
                //如果登录信息有更新, 那么更新数据库
                if (!isRegister && accountInfo != newAccountInfo)
                {
                    await _accountRepository.UpdateAsync(accountInfo);
                }
                //设置登录信息
                await _redis.SetStringAsync<LoginCheckInfo>(KeyGenHelper.GenKey(request.Info.PlatformAccount, typeof(LoginCheckInfo).Name), 
                    new LoginCheckInfo(accountInfo.Id, newAccountInfo.PlatformAccount, newAccountInfo.Type), 
                    TimeSpan.FromDays(7));
                //设置玩家信息缓存
                return new HasBodyResponse<AccountResponse>(StatuCodeDefines.Success, null, new AccountResponse(accountInfo.Id,
                    accountInfo.PlatformAccount,
                    newAccountInfo.UserName,
                    newAccountInfo.Sex,
                    newAccountInfo.HeadUrl,
                    ""));
            }

            HasBodyResponse<AccountResponse> response = new HasBodyResponse<AccountResponse>(StatuCodeDefines.LoginError,
                null, null);
            
            return response;

        }
    }
}
