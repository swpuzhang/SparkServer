using Commons.Domain.Bus;
using Commons.Domain.Commands;
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
    }
}
