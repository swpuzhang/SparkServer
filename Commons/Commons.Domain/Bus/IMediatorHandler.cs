using Commons.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Domain.Bus
{
    public interface IMediatorHandler
    {
        Task<TResponse> SendCommand<TResponse>(Command<TResponse> command);
    }
}
