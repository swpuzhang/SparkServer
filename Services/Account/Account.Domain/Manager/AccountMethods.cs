using Account.Domain.Models;
using Account.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Manager
{

    public static class AccountMethods
    {
        public static async Task<AccountInfo> GetAccountInfo(long id, IAccountInfoRepository _accountRepository, IAccountRedisRepository _redis)
        {
            var accountInfo = await _redis.GetAccountInfo(id);
            if (accountInfo == null)
            {
                accountInfo = await _accountRepository.GetByIdAsync(id);
                if (accountInfo != null)
                {
                    await _redis.SetAccountInfo(accountInfo);
                }
            }
            return accountInfo;
        }

        public static async Task<long?> GetIdByPlatForm(string platformId, IAccountInfoRepository _accountRepository, IAccountRedisRepository _redis)
        {
            var checkInfo = await _redis.GetLoginCheckInfo(platformId);
            AccountInfo accountInfo = null;
            if (checkInfo == null)
            {
                accountInfo = await _accountRepository.GetByPlatform(platformId);
                if (accountInfo != null)
                {
                    await _redis.SetLoginCheckInfo(platformId, accountInfo);
                    return accountInfo.Id;
                }
                return null;
            }
            return checkInfo.Id;
        }
    }
}
