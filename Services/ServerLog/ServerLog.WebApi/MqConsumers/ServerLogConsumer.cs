using Commons.IntegrationBus.MqCommands.Sangong;
using Commons.Message.MqEvents;
using MassTransit;
using ServerLog.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerLog.WebApi.MqConsumers
{
    public class ServerLogConsumer :
        IConsumer<GameLogMqCommand>,
        IConsumer<MoneyChangedMqEvent>
    {

        IServerLogService _service;
        public ServerLogConsumer(IServerLogService service)
        {
            _service = service;
        }

        public  Task Consume(ConsumeContext<GameLogMqCommand> context)
        {
             return _service.WriteGameLog(context.Message);
        }

        public Task Consume(ConsumeContext<MoneyChangedMqEvent> context)
        {
            return _service.WriteMoneyLog(context.Message);
        }
    }
}
