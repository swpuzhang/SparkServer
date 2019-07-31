using Commons.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Commons.Infrastruct
{
    public interface IRpcCaller<THub>
    {
        Task<BaseResponse> RequestCallAsync(string conn, string method, 
            string reqData, Guid id, int waitMiliSeconds = 5000);
        void OnResponsed(Guid id);
    }
}
