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

namespace Sangong.Domain.Manager
{
   


    public class MathcingManager
    {
        private List<CoinsRangeConfig> _coinsRangeCfg;
        private IConfigRepository _configRespository;
        private ISangongRedisRepository _redis;
        private IRequestClient<JoinGameRoomMqCommand> _roomClient;
        private RoomManager _roomManager;
        public MathcingManager(IConfigRepository configRespository,
            ISangongRedisRepository redis,
            IRequestClient<JoinGameRoomMqCommand> roomClient, 
            RoomManager roomManager)
        {
            _configRespository = configRespository;
            _redis = redis;
            _roomClient = roomClient;
            _roomManager = roomManager;
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

        public async Task<BodyResponse<SangongMatchingResponseInfo>> MatchingRoom(long id, long blind, int curRoomId = 0)
        {

            //获取这个玩家的redis锁
            using (var locker = _redis.Loker(KeyGenHelper.GenUserKey(id, UserRoomInfo.className)))
            {
                if (!locker.TryLock())
                {
                    return new BodyResponse<SangongMatchingResponseInfo>(StatuCodeDefines.IsMatching, null, null);
                }
                BaseResponse roomResponse = null;
                //查询redis是否有这个玩家
                var userRoomInfo = await _redis.GetUserRoomInfo(id);
                if (userRoomInfo != null)
                {
                    if (userRoomInfo.Status == MatchingStatus.Success)
                    {
                        return new BodyResponse<SangongMatchingResponseInfo>(StatuCodeDefines.Success, null,
                            new SangongMatchingResponseInfo(id, userRoomInfo.RoomId, userRoomInfo.Blind, userRoomInfo.GameKey));
                    }
                    //如果正在匹配说明上次程序异常，或者内网出现问题，尝试进行在一次匹配
                    roomResponse = await SendToGameRoom(curRoomId, userRoomInfo.GameKey, id);

                }
                else
                {
                    //查找等待人数最多的房间

                    var roomInfo = await _roomManager.GetRoom(blind);
                }
                return new BodyResponse<SangongMatchingResponseInfo>(StatuCodeDefines.Success, null,
                            new SangongMatchingResponseInfo(id, userRoomInfo.RoomId, userRoomInfo.Blind, userRoomInfo.GameKey));

            }

            
            
           
        }

        public async Task<BaseResponse>  SendToGameRoom(int roomId, string gameKey, long id)
        {
            var response = await _roomClient.GetResponseExt<JoinGameRoomMqCommand, BaseResponse>
                (new JoinGameRoomMqCommand(id, roomId, gameKey));
            return response.Message;
        }
    }
}
