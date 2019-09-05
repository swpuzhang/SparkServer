using GameSample.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using GameSample.Domain.RepositoryInterface;
using System.Linq;
using Commons.Domain.Models;
using System.Threading.Tasks;
using Commons.Extenssions.Defines;
using MassTransit;
using GameSample.MqCommands;
using Commons.Extenssions;
using Serilog;
using Microsoft.Extensions.Configuration;

namespace GameSample.Domain.Manager
{
   


    public class MatchingManager
    {
        private List<CoinsRangeConfig> _coinsRangeCfg;
        private IConfigRepository _configRespository;
        private IGameSampleRedisRepository _redis;
        public static string matchingGroup;


        private RoomManager _roomManager;
        public MatchingManager(IConfiguration config,  IConfigRepository configRespository,
            IGameSampleRedisRepository redis,
            RoomManager roomManager)
        {
            _configRespository = configRespository;
            _redis = redis;
            _roomManager = roomManager;
            matchingGroup = config["MatchingGroup"];
        }

        public void  LoadCoinsRangeConfig()
        {
             _coinsRangeCfg = _configRespository.LoadCoinsRangeConfig();
        }

        public bool GetBlindFromCoins(long coins, out long blind)

        {
            blind = 0;
            foreach (var oneCfg in _coinsRangeCfg)
            {
                if (coins >= oneCfg.CoinsBegin && coins < (oneCfg.CoinsEnd == -1 ? long.MaxValue : oneCfg.CoinsEnd))
                {
                    blind = oneCfg.Blind;
                    return true;
                }
            }
            return false;
        }

        public async Task<BodyResponse<GameSampleMatchingResponseInfo>> MatchingRoom(long id, long blind, string curRoomId)
        {

            //获取这个玩家的redis锁
            using (var locker = _redis.Locker(KeyGenHelper.GenUserKey(id, UserRoomInfo.className)))
            {
                if (!await locker.TryLockAsync())
                {
                    return new BodyResponse<GameSampleMatchingResponseInfo>(StatusCodeDefines.IsMatching, null, null);
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
                    
                    if (roomResponse.StatusCode == StatusCodeDefines.Success)
                    {
                        _ = _redis.SetUserRoomInfo(new UserRoomInfo(id, roomInfo.RoomId, roomInfo.GameKey, blind, MatchingStatus.Success));
                        return new BodyResponse<GameSampleMatchingResponseInfo>(StatusCodeDefines.Success, null,
                            new GameSampleMatchingResponseInfo(id, roomInfo.RoomId, roomInfo.Blind, roomInfo.GameKey));
                    }
                    else
                    {
                        if (roomResponse.Body != null)
                        {
                            RoomInfo newRoomInfo = new RoomInfo(roomResponse.Body.RoomId, roomResponse.Body.UserCount, 
                                roomResponse.Body.GameKey, roomResponse.Body.Blind);
                            _roomManager.UpdateRoom(newRoomInfo);
                        }
                        
                        _ = _redis.DeleteUserRoomInfo(id);
                        return new BodyResponse<GameSampleMatchingResponseInfo>(roomResponse.StatusCode, roomResponse.ErrorInfos, null);
                    }
                }

                catch
                {
                    _ = _redis.DeleteUserRoomInfo(id);
                    Log.Error($"user {id} join room {roomInfo.RoomId} error");
                    return new BodyResponse<GameSampleMatchingResponseInfo>(StatusCodeDefines.BusError, null, null);
                }
            }
        }

        public  Task OnJoinGame(long id, string gameKey, string roomId, long blind, int userCount, string group)
        {
            /*if (group != matchingGroup)
            {
                return;
            }

            using (var locker = _redis.Locker(KeyGenHelper.GenUserKey(id, UserRoomInfo.className)))
            {
                await locker.LockAsync();
                _ = _redis.SetUserRoomInfo(new UserRoomInfo(id, roomId, gameKey, blind, MatchingStatus.Success));
                
            }
                
            _roomManager.OnUserCountChange(gameKey, roomId, blind, userCount);*/
            return Task.CompletedTask;
        }

        public async Task OnLeaveGame(long id, string gameKey, string roomId, long blind, int userCount, string group)
        {
            if (group != matchingGroup)
            {
                return;
            }

            using (var locker = _redis.Locker(KeyGenHelper.GenUserKey(id, UserRoomInfo.className)))
            {
                await locker.LockAsync();
                _ = _redis.DeleteUserRoomInfo(id);
            }
            _roomManager.OnUserCountChange(gameKey, roomId, blind, -1);
        }

        public async Task<BodyResponse<NullBody>> OnUserApplySit(long id, string gameKey, Int64 blind, string roomId)
        {
            using (var locker = _redis.Locker(KeyGenHelper.GenUserKey(id, UserRoomInfo.className)))
            {
                await locker.LockAsync();
                var userRoomInfo = await _redis.GetUserRoomInfo(id);
                if (userRoomInfo != null)
                {
                    return new BodyResponse<NullBody>(StatusCodeDefines.Error, new List<string>() { "user already in room " });
                }
                if (!_roomManager.JoinOneRoom(gameKey, roomId))
                {
                    return new BodyResponse<NullBody>(StatusCodeDefines.Error, new List<string>() { "room is full " });
                }
                _ = _redis.SetUserRoomInfo(new UserRoomInfo(id, roomId, gameKey, blind, MatchingStatus.Success));
                return new BodyResponse<NullBody>(StatusCodeDefines.Success, null);
            }
        }

        public async Task OnSiteFailed(long id, string gameKey, string roomId, string group)
        {

            if (group != matchingGroup)
            {
                return;
            }
            using (var locker = _redis.Locker(KeyGenHelper.GenUserKey(id, UserRoomInfo.className)))
            {
                await locker.LockAsync();
                _ = _redis.DeleteUserRoomInfo(id);
                _roomManager.JoinOneRoomFailed(gameKey, roomId);
            }
        }
    }
}
