using Commons.Domain.Bus;
using Commons.Domain.Commands;
using Commons.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Infrastruct
{
    public class InProcessBus : IMediatorHandler
    {

        private readonly IMediator _mediator;

        public InProcessBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TResponse> SendCommand<TResponse>(Command<TResponse> command)
        {
            return await _mediator.Send(command);
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            //if (!@event.MessageType.Equals("DomainNotification"))
            //    _eventStore?.Save(@event);

            return _mediator.Publish(@event);
        }
    }
}
