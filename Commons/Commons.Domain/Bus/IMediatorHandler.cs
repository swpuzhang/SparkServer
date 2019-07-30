using Commons.Domain.Commands;
using Commons.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Domain.Bus
{
    public interface IMediatorHandler
    {
        Task<TResponse> SendCommand<TResponse>(Command<TResponse> command);
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
