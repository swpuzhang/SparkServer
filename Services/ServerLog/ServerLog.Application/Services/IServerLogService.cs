using ServerLog.Application.ViewModels;
using ServerLog.Domain.Models;
using Commons.Domain.Models;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Commons.IntegrationBus.MqCommands.Sangong;
using Commons.Message.MqEvents;

namespace ServerLog.Application.Services
{
    public interface IServerLogService
    {

        Task WriteGameLog(GameLogMqCommand command);
        Task WriteMoneyLog(MoneyChangedMqEvent command);
    }
}
