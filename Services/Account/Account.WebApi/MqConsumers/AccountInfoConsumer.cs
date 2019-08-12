using Account.Application.Services;
using Commons.IntegrationBus.MqEvents;
using Commons.MqEvents;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons.MqCommands;
using Commons.Extenssions.Defines;
using Commons.Domain.Models;

namespace Account.WebApi.MqConsumers
{
    public class GetAccountInfoConsumer :
        IConsumer<GetAccountInfoMqCommand>
    {
        private IAccountAppService _service;

        public GetAccountInfoConsumer(IAccountAppService service)
        {
            _service = service;
        }

        

        public async Task Consume(ConsumeContext<GetAccountInfoMqCommand> context)
        {
            var response = await _service.GetSelfAccount(context.Message.Id);
            if (response.StatusCode != StatuCodeDefines.Success)
            {
                await context.RespondAsync<BodyResponse<GetAccountInfoMqResponse>>(new BodyResponse<GetAccountInfoMqResponse>(response.StatusCode,
                    null, null));
                return;
            }
            await context.RespondAsync<BodyResponse<GetAccountInfoMqResponse>>(new BodyResponse<GetAccountInfoMqResponse>(StatuCodeDefines.Success, null, new GetAccountInfoMqResponse(context.Message.Id,
                response.Body.PlatformAccount, response.Body.UserName, response.Body.Sex, response.Body.HeadUrl,
                new GameInfoMq(response.Body.GameInfo.GameTimes, response.Body.GameInfo.WinTimes, response.Body.GameInfo.MaxWinCoins),
                new LevelInfoMq(response.Body.LevelInfo.CurLevel, response.Body.LevelInfo.CurExp, response.Body.LevelInfo.NeedExp))));
        }
    }
}
