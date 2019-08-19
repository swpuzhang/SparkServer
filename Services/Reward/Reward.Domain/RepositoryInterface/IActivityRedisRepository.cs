using Commons.Domain.RepositoryInterface;
using Reward.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reward.Domain.RepositoryInterface
{
    public interface IActivityRedisRepository : IRedisRepository
    {
        Task<OneGameActivityInfo> GetGameActivity(DateTime time, long id, string activityId);
        Task<GameSubActInfo> GetGameActProgress(DateTime time, long id, string activityId, string subId);
        Task SetGameActProgress(DateTime time, long id, string activityId, string subId, GameSubActInfo subAct);
    }
}
