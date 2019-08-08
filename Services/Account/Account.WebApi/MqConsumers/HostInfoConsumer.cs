using Account.Application.Services;
using Commons.IntegrationBus.MqEvents;
using Commons.MqEvents;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.WebApi.MqConsumers
{
    public class HostInfoConsumer :
        IConsumer<HostInfoEvent>
    {
        private IMqService _service;

        public HostInfoConsumer(IMqService service)
        {
            _service = service;
        }

        public Task Consume(ConsumeContext<HostInfoEvent> context)
        {
            _service.NotifyHostInfo(context.Message.Host, context.Message.UserCount);
            return Task.CompletedTask;
        }
    }
}
