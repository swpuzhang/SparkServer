using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Commons.Extenssions
{
    public static class MassTrasitExtenssion
    {
        public static Task<Response<T>> GetResponseExt<TRequest, T>(this IRequestClient<TRequest> client, TRequest message,
            int msTimeout = 3000) where T : class 
            where TRequest : class
        {
            return client.GetResponse<T>(message, timeout: RequestTimeout.After(0, 0, 0, 0, msTimeout));
        }
    }

    
}
