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

namespace InterfaceDemo.Controllers
{
    /// <summary>
    /// 三公房间内的消息， 只能看不能调用, 向websocket 发送AppRoomRequest消息名的消息. 
    /// 所有消息都是向wbsocketserver发送AppRoomRequest消息, 只是不同消息ReqName 和ReqData不一样
    /// 这里只是为了标识不同的接口,比如AppRoomRequestApplyStandupCommand就是向server发送
    /// AppRoomRequest消息, ReqName = ApplyStandupCommand, ReqData = ApplyStandupCommand
    /// </summary>
    [Route("[Action]")]
    [ApiController]
    public class WSSangongGameController : ControllerBase
    {
        /// <summary>
        /// 请求站起
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public ToAppResponse<NullBody> AppRoomRequestApplyStandupCommand(AppRoomRequest<ApplyStandupCommand> command)
        {
            return new ToAppResponse<NullBody>();
        }

        /// <summary>
        /// 请求离开
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public ToAppResponse<NullBody> AppRoomRequestApplyLeaveCommand(AppRoomRequest<ApplyLeaveCommand> command)
        {
            return new ToAppResponse<NullBody>();
        }

        /// <summary>
        /// 请求坐下
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public ToAppResponse<NullBody> AppRoomRequestApplySitdownCommand(AppRoomRequest<ApplySitdownCommand> command)
        {
            return new ToAppResponse<NullBody>();
        }

        /// <summary>
        /// 请求弃牌
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public ToAppResponse<NullBody> AppRoomRequestApplyDropCommand(AppRoomRequest<ApplyDropCommand> command)
        {
            return new ToAppResponse<NullBody>();
        }

        /// <summary>
        /// 请求过牌
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public ToAppResponse<NullBody> AppRoomRequestApplyPassCommand(AppRoomRequest<ApplyPassCommand> command)
        {
            return new ToAppResponse<NullBody>();
        }

        /// <summary>
        /// 请求跟牌
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public ToAppResponse<NullBody> AppRoomRequestApplyFollowCommand(AppRoomRequest<ApplyFollowCommand> command)
        {
            return new ToAppResponse<NullBody>();
        }

        /// <summary>
        /// 请求加注
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public ToAppResponse<NullBody> AppRoomRequestApplyAddCommand(AppRoomRequest<ApplyAddCommand> command)
        {
            return new ToAppResponse<NullBody>();
        }

        /// <summary>
        /// 请求同步房间
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public ToAppResponse<ApplySyncGameRoomResponse> AppRoomRequestApplySyncGameRoomCommand(AppRoomRequest<ApplySyncGameRoomCommand> command)
        {
            return new ToAppResponse<ApplySyncGameRoomResponse>();
        }
    }

    /// <summary>
    /// 三公房间内的通知事件 只能看不能调用,  向APP通知ToAppRoomRequest消息名的消息
    /// 只能看不能调用, server向app发送 ToAppRoomRequest. 
    /// 所有消息都是向app发送ToAppRoomRequest消息, 只是不同消息ReqName 和ReqData不一样
    /// 这里只是为了标识不同的接口,比如ToAppRoomRequestPlayerSeatedEvent就是向app发送
    /// ToAppRoomRequest消息, ReqName = PlayerSeatedEvent, ReqData = PlayerSeatedEvent
    /// </summary>
    [Route("[Action]")]
    [ApiController]
    public class WSSangongGameNotifyController : ControllerBase
    {
        /// <summary>
        /// 广播玩家坐下
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public ToServerResponse<PlayerSeatedEvent> ToAppRoomRequestPlayerSeatedEvent(ToAppRoomRequest<PlayerSeatedEvent> command)
        {
            return new ToServerResponse<PlayerSeatedEvent>();
        }

        /// <summary>
        /// 广播发牌消息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public ToServerResponse<DealCardsEvent> ToAppRoomRequestDealCardsEvent(ToAppRoomRequest<DealCardsEvent> command)
        {
            return new ToServerResponse<DealCardsEvent>();
        }


        /// <summary>
        /// 广播激活玩家操作消息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public ToServerResponse<ActiveEvent> ToAppRoomRequestActiveEvent(ToAppRoomRequest<ActiveEvent> command)
        {
            return new ToServerResponse<ActiveEvent>();
        }

        /// <summary>
        /// 广播发第三张牌消息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public ToServerResponse<DealThirdCardEvent> ToAppRoomRequestDealThirdCardEvent(ToAppRoomRequest<DealThirdCardEvent> command)
        {
            return new ToServerResponse<DealThirdCardEvent>();
        }

        /// <summary>
        /// 广播弃牌消息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public ToServerResponse<DropEvent> ToAppRoomRequestDropEvent(ToAppRoomRequest<DropEvent> command)
        {
            return new ToServerResponse<DropEvent>();
        }

        /// <summary>
        /// 广播过牌消息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public ToServerResponse<PassEvent> ToAppRoomRequestPassEvent(ToAppRoomRequest<PassEvent> command)
        {
            return new ToServerResponse<PassEvent>();
        }

        /// <summary>
        /// 广播跟牌消息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public ToServerResponse<FollowEvent> ToAppRoomRequestFollowEvent(ToAppRoomRequest<FollowEvent> command)
        {
            return new ToServerResponse<FollowEvent>();
        }

        /// <summary>
        /// 广播加注消息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public ToServerResponse<AddEvent> ToAppRoomRequestAddEvent(ToAppRoomRequest<AddEvent> command)
        {
            return new ToServerResponse<AddEvent>();
        }

        /// <summary>
        /// 广播结算消息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public ToServerResponse<GameOverEvent> ToAppRoomRequestGameOverEvent(ToAppRoomRequest<GameOverEvent> command)
        {
            return new ToServerResponse<GameOverEvent>();
        }

        /// <summary>
        /// 广播买入消息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public ToServerResponse<PlayerStanupEvent> ToAppRoomRequestPlayerStanupEvent(ToAppRoomRequest<PlayerStanupEvent> command)
        {
            return new ToServerResponse<PlayerStanupEvent>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public ToServerResponse<PlayerBuyInEvent> ToAppRoomRequestPlayerBuyInEvent(ToAppRoomRequest<PlayerBuyInEvent> command)
        {
            return new ToServerResponse<PlayerBuyInEvent>();
        }
        
    }
}