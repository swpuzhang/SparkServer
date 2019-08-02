using Sangong.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MassTransit;
using Sangong.MqCommands;
using Microsoft.Extensions.Configuration;
using Commons.Domain.Models;
using Commons.Extenssions.Defines;
using System.Threading.Tasks;
using Commons.Extenssions;

namespace Sangong.Domain.Manager
{
    public class OneRoomGroup
    {
        public RoomInfo GetEmptyRoom(long blind)
        {
            if (!_emptyQueues.TryGetValue(blind , out var queue))
            {
                return null;
            }
            if (queue.Count == 0)
            {
                return null;
            }
            return queue.Dequeue();
        }
        private Dictionary<int, RoomInfo> _allRooms = new Dictionary<int, RoomInfo>();
        private Dictionary<long, Queue<RoomInfo>> _emptyQueues = new Dictionary<long, Queue<RoomInfo>>();
        public int UserCount { get; private set; }
    }

    public class RoomManager
    {
        private IBusControl _bus;
        private Dictionary<long, SortedSet<RoomInfo>> _matchingQueue = new Dictionary<long, SortedSet<RoomInfo>>();
        Dictionary<string, OneRoomGroup> _roomGroups = new Dictionary<string, OneRoomGroup>();

        public static string mqUri = string.Empty;
        private Dictionary<long, BlindConfig> _roomConfig;
        private AsyncSemaphore _semaphpre = new AsyncSemaphore();
        public RoomManager(IBusControl bus, IConfiguration configuration)
        {
            _bus = bus;
            var rabbitCfg = configuration.GetSection("rabbitmq");
            mqUri = $"rabbitmq://{rabbitCfg["host"]}/{rabbitCfg["vhost"]}";
        }

        /// <summary>
        /// 获取人数最少的那个组
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<string, OneRoomGroup> GetLeastGroup()
        {
           
            int UserCount = 0;
            if (_roomGroups.Count == 0)
            {
                throw new Exception("have no game started");
            }
            KeyValuePair<string, OneRoomGroup> oneGroup = _roomGroups.First();
            foreach (var one in _roomGroups)
            {
                if (one.Value.UserCount > UserCount)
                {
                    UserCount = one.Value.UserCount;
                    oneGroup = one;
                }
            }
            return oneGroup;
        }

        public async Task<RoomInfo> CreateRoom(string gameKey, long blind)
        {
            int roomId = 0;
            //生成roomId
            if (!_roomConfig.TryGetValue(blind, out var blindConfig))
            {
                throw new Exception($"have no blind{blind} room");
            }

            var busClient = _bus.CreateRequestClient<CreateRoomMqCommand>(new Uri($"{mqUri}/{gameKey}"), TimeSpan.FromSeconds(3));
            
            var busResponse = await busClient.GetResponseExt<CreateRoomMqCommand, BaseResponse>
                (new CreateRoomMqCommand(roomId, gameKey,  blind, blindConfig.MinCoins,
                blindConfig.MaxCoins, blindConfig.TipsPersent));
            if (busResponse.Message.StatusCode != StatuCodeDefines.Success)
            {
                throw new Exception($"Create Room {roomId} blind{blind} error");
            }
            return new RoomInfo(roomId, 0, gameKey);
        }

        public async Task<RoomInfo> GetRoom(long blind)
        {
            using (await _semaphpre.WaitAsync())
            {
                if (_roomGroups.Count == 0)
                {
                    throw new Exception("have no game started");
                }
                RoomInfo roomInfo = null;
                //先查看已经匹配的队列是否有合适的房间
                if (!_matchingQueue.TryGetValue(blind, out var matchingRooms) || matchingRooms.Count == 0)
                {
                    //查找空闲房间
                    var oneGroup = GetLeastGroup();

                    roomInfo = oneGroup.Value.GetEmptyRoom(blind);
                    if (roomInfo == null)
                    {
                        //创建新房间
                        roomInfo = await CreateRoom(oneGroup.Key, blind);
                    }
                    return roomInfo;
                }

                roomInfo = matchingRooms.First<RoomInfo>();
                if (roomInfo.IsFull() || roomInfo.IsEmpty())
                {
                    throw new Exception("matchingRooms matching queue error");
                }
                return roomInfo;
            }
            //还没有任何房间启动
           
        }
    }
}
