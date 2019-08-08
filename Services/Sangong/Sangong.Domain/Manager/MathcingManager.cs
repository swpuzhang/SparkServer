using Sangong.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Sangong.Domain.RepositoryInterface;
using System.Linq;
using Commons.Domain.Models;
using System.Threading.Tasks;
using Commons.Extenssions.Defines;
using MassTransit;
using Sangong.MqCommands;
using Commons.Extenssions;
using Serilog;
using Microsoft.Extensions.Configuration;

namespace Sangong.Domain.Manager
{
   


    public class MatchingManager
    {
        private List<CoinsRangeConfig> _coinsRangeCfg;
        private IConfigRepository _configRespository;
        private ISangongRedisRepository _redis;
        public static string matchingGroup;


        private RoomManager _roomManager;
        public MatchingManager(IConfiguration config,  IConfigRepository configRespository,
            ISangongRedisRepository redis,
            RoomManager roomManager)
        {
            _configRespository = configRespository;
            _redis = redis;
            _roomManager = roomManager;
            matchingGroup = config["MatchingGroup"];
        }

        public void LoadCoinsRangeConfig()
        {
            _coinsRangeCfg = _configRespository.LoadCoinsRangeConfig();
        }

        public long GetBlindFromCoins(long coins)
        {
            foreach (var oneCfg in _coinsRangeCfg)
            {
                if (coins >= oneCfg.CoinsBegin && coins < oneCfg.CoinsEnd)
                {
                    return oneCfg.Blind;
                }
            }
            return _coinsRangeCfg.Last().Blind;
        }

        public async Task<BodyResponse<SangongMatchingResponseInfo>> MatchingRoom(long id, long blind, string curRoomId)
        {

            //获取这个玩家的redis锁
            using (var locker = _redis.Loker(KeyGenHelper.GenUserKey(id, UserRoomInfo.className)))
            {
                if (!await locker.TryLockAsync())
                {
                    return new BodyResponse<SangongMatchingResponseInfo>(StatuCodeDefines.IsMatching, null, null);
                }

                //查询redis是否有这个玩家
                RoomInfo roomInfo = null;
                var userRoomInfo = await _redis.GetUserRoomInfo(id);
                if (userRoomInfo != null)
                {
                    roomInfo = new RoomInfo(userRoomInfo.RoomId, 0, userRoomInfo.GameKey, userRoomInfo.Blind);
                }
                else
                {
                    roomInfo = await _roomManager.GetRoom(id, blind);
                }
                try
                {
                    BodyResponse<JoinGameRoomMqResponse> roomResponse = 
                        await _roomManager.SendToGameRoom<JoinGameRoomMqCommand, BodyResponse<JoinGameRoomMqResponse>>
                        (roomInfo.GameKey, new JoinGameRoomMqCommand(id, roomInfo.RoomId, roomInfo.GameKey));
                    RoomInfo newRoomInfo = new RoomInfo(roomResponse.Body.RoomId, roomResponse.Body.UserCount, roomResponse.Body.GameKey, roomResponse.Body.Blind);
                    _roomManager.UpdateRoom(newRoomInfo);
                    if (roomResponse.StatusCode == StatuCodeDefines.Success)
                    {
                        _ = _redis.SetUserRoomInfo(new UserRoomInfo(id, roomInfo.RoomId, roomInfo.GameKey, blind, MatchingStatus.Success));
                        return new BodyResponse<SangongMatchingResponseInfo>(StatuCodeDefines.Success, null,
                            new SangongMatchingResponseInfo(id, roomInfo.RoomId, roomInfo.Blind, roomInfo.GameKey));
                    }
                    else
                    {
                        _ = _redis.DeleteUserRoomInfo(id);
                        return new BodyResponse<SangongMatchingResponseInfo>(roomResponse.StatusCode, roomResponse.ErrorInfos, null);
                    }
                }

                catch
                {
                    _ = _redis.DeleteUserRoomInfo(id);
                    Log.Error($"user {id} join room {roomInfo.RoomId} error");
                    return new BodyResponse<SangongMatchingResponseInfo>(StatuCodeDefines.BusError, null, null);
                }
            }
        }

        public async Task OnJoinGame(long id, string gameKey, string roomId, long blind, int userCount, string group)
        {
            if (group != matchingGroup)
            {
                return;
            }

            using (var locker = _redis.Loker(KeyGenHelper.GenUserKey(id, UserRoomInfo.className)))
            {
                await locker.LockAsync();
                _ = _redis.SetUserRoomInfo(new UserRoomInfo(id, roomId, gameKey, blind, MatchingStatus.Success));
                
            }
                
            _roomManager.OnUserCountChange(gameKey, roomId, blind, userCount);
        }

        public async Task OnLeaveGame(long id, string gameKey, string roomId, long blind, int userCount, string group)
        {
            if (group != matchingGroup)
            {
                return;
            }

            using (var locker = _redis.Loker(KeyGenHelper.GenUserKey(id, UserRoomInfo.className)))
            {
                await locker.LockAsync();
                _ = _redis.DeleteUserRoomInfo(id);

            }
            _roomManager.OnUserCountChange(gameKey, roomId, blind, userCount);
        }

        public async Task<BaseResponse> OnUserApplySit(long id, string gameKey, string roomId)
        {
            using (var locker = _redis.Loker(KeyGenHelper.GenUserKey(id, UserRoomInfo.className)))
            {
                await locker.LockAsync();

              
                var userRoomInfo = await _redis.GetUserRoomInfo(id);
                if (userRoomInfo != null)
                {

                    return new BaseResponse(StatuCodeDefines.Error, new List<string>() { "user already in room " });
                }
                if (!_roomManager.JoinOneRoom(gameKey, roomId))
                {
                    return new BaseResponse(StatuCodeDefines.Error, new List<string>() { "room is full " });
                }
                return new BaseResponse(StatuCodeDefines.Success, null);
            }
        }
    }
}
