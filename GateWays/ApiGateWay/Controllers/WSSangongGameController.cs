using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account.Application.Services;
using Account.Application.ViewModels;
using Commons.Infrastruct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commons.Domain.Models;
using Account.Domain.Models;
using Commons.Extenssions.Defines;
using Commons.Extenssions;
using Sangong.GameMessage;

namespace ApiGateWay.Controllers
{
    /// <summary>
    /// 三公房间内的消息， 只能看不能调用
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class WSSangongGameController : ControllerBase
    {
        /// <summary>
        /// 请求站起
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ToAppResponse<NullBody> ApplyStandupCommand(AppRoomRequest<ApplyStandupCommand> command)
        {
            return new ToAppResponse<NullBody>();
        }

        /// <summary>
        /// 请求离开
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ToAppResponse<NullBody> ApplyLeaveCommand(AppRoomRequest<ApplyLeaveCommand> command)
        {
            return new ToAppResponse<NullBody>();
        }

        /// <summary>
        /// 请求坐下
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ToAppResponse<NullBody> ApplySitdownCommand(AppRoomRequest<ApplySitdownCommand> command)
        {
            return new ToAppResponse<NullBody>();
        }

        /// <summary>
        /// 请求弃牌
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ToAppResponse<NullBody> ApplyDropCommand(AppRoomRequest<ApplyDropCommand> command)
        {
            return new ToAppResponse<NullBody>();
        }

        /// <summary>
        /// 请求过牌
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ToAppResponse<NullBody> ApplyPassCommand(AppRoomRequest<ApplyPassCommand> command)
        {
            return new ToAppResponse<NullBody>();
        }

        /// <summary>
        /// 请求跟牌
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ToAppResponse<NullBody> ApplyFollowCommand(AppRoomRequest<ApplyFollowCommand> command)
        {
            return new ToAppResponse<NullBody>();
        }

        /// <summary>
        /// 请求加注
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ToAppResponse<NullBody> ApplyAddCommand(AppRoomRequest<ApplyAddCommand> command)
        {
            return new ToAppResponse<NullBody>();
        }

        /// <summary>
        /// 请求同步房间
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ToAppResponse<ApplySyncGameRoomResponse> ApplySyncGameRoomCommand(AppRoomRequest<ApplySyncGameRoomCommand> command)
        {
            return new ToAppResponse<ApplySyncGameRoomResponse>();
        }
    }

    /// <summary>
    /// 三公房间内的通知事件
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class WSSangongGameNotifyController : ControllerBase
    {
        /// <summary>
        /// 广播玩家坐下
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ToServerResponse<PlayerSeatedEvent> PlayerSeatedEvent(ToAppRoomRequest<PlayerSeatedEvent> command)
        {
            return new ToServerResponse<PlayerSeatedEvent>();
        }

        /// <summary>
        /// 广播发牌消息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ToServerResponse<DealCardsEvent> DealCardsEvent(ToAppRoomRequest<DealCardsEvent> command)
        {
            return new ToServerResponse<DealCardsEvent>();
        }


        /// <summary>
        /// 广播激活玩家操作消息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ToServerResponse<ActiveEvent> DealCardsEvent(ToAppRoomRequest<ActiveEvent> command)
        {
            return new ToServerResponse<ActiveEvent>();
        }

        /// <summary>
        /// 广播发第三张牌消息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ToServerResponse<DealThirdCardEvent> DealCardsEvent(ToAppRoomRequest<DealThirdCardEvent> command)
        {
            return new ToServerResponse<DealThirdCardEvent>();
        }

        /// <summary>
        /// 广播弃牌消息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ToServerResponse<DropEvent> DealCardsEvent(ToAppRoomRequest<DropEvent> command)
        {
            return new ToServerResponse<DropEvent>();
        }

        /// <summary>
        /// 广播过牌消息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ToServerResponse<PassEvent> DealCardsEvent(ToAppRoomRequest<PassEvent> command)
        {
            return new ToServerResponse<PassEvent>();
        }

        /// <summary>
        /// 广播跟牌消息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ToServerResponse<FollowEvent> FollowEvent(ToAppRoomRequest<FollowEvent> command)
        {
            return new ToServerResponse<FollowEvent>();
        }

        /// <summary>
        /// 广播加注消息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ToServerResponse<AddEvent> DealCardsEvent(ToAppRoomRequest<AddEvent> command)
        {
            return new ToServerResponse<AddEvent>();
        }

        /// <summary>
        /// 广播结算消息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ToServerResponse<GameOverEvent> DealCardsEvent(ToAppRoomRequest<GameOverEvent> command)
        {
            return new ToServerResponse<GameOverEvent>();
        }

        /// <summary>
        /// 广播买入消息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ToServerResponse<PlayerStanupEvent> DealCardsEvent(ToAppRoomRequest<PlayerStanupEvent> command)
        {
            return new ToServerResponse<PlayerStanupEvent>();
        }

        public ToServerResponse<PlayerBuyInEvent> DealCardsEvent(ToAppRoomRequest<PlayerBuyInEvent> command)
        {
            return new ToServerResponse<PlayerBuyInEvent>();
        }
        
    }
}