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
            //从改底注获取一个空闲队列
            if (!_emptyQueues.TryGetValue(blind , out var queue))
            {
                return null;
            }
            if (queue.Count == 0)
            {
                return null;
            }
            //出队
            queue.Remove(queue.First().Key, out var value);
            return value;
        }
        public void CreatRoom(RoomInfo roomInfo)
        {
            _allRooms.Add(roomInfo.RoomId, roomInfo);
        }

        public RoomInfo FindRoom(string roomId)
        {
            _allRooms.TryGetValue(roomId, out var value);
            return value;
        }

        public RoomInfo RemoveEmptyRoom(long blind, string roomId)
        {
            _emptyQueues.TryGetValue(blind, out var rooms);
            if (rooms != null)
            {
                rooms.Remove(roomId, out var value);
                return value;
            }
            return null;
        }

        public void AddEmptyRoom( RoomInfo info)
        {
            _emptyQueues.TryGetValue(info.Blind, out var rooms);
            if (rooms == null)
            {
                rooms = new Dictionary<string, RoomInfo>();

                _emptyQueues.Add(info.Blind, rooms);
            }
            rooms.Add(info.RoomId, info);
           
        }

        private Dictionary<string, RoomInfo> _allRooms = new Dictionary<string, RoomInfo>();
        private Dictionary<long, Dictionary<string, RoomInfo>> _emptyQueues = new Dictionary<long, Dictionary<string, RoomInfo>>();
        public int UserCount { get; private set; }
       
        public List<RoomInfo> RemoveNoExist(List<SyncRoomInfo> rooms)
        {
            List<RoomInfo> deletRooms = new List<RoomInfo>();
            foreach (var room in _allRooms)
            {
                if (rooms.Find(x => x.RoomId == room.Key) == null)
                {
                    deletRooms.Add(room.Value);
                }
            }
            foreach (var deletroom in deletRooms)
            {
                _allRooms.Remove(deletroom.RoomId);
                if (_emptyQueues.TryGetValue(deletroom.Blind, out var emptyRooms))
                {
                    emptyRooms.Remove(deletroom.RoomId);
                }
            }
            
            return deletRooms;
        }
    }

    public class RoomManager
    {
        private IBusControl _bus;
        private Dictionary<long, SortedSet<RoomInfo>> _matchingQueue = new Dictionary<long, SortedSet<RoomInfo>>();
        Dictionary<string, OneRoomGroup> _roomGroups = new Dictionary<string, OneRoomGroup>();
        
        public static string mqUri = string.Empty;
        private Dictionary<long, BlindConfig> _roomConfig = new Dictionary<long, BlindConfig>();
        private AsyncSemaphore _semaphpre = new AsyncSemaphore();

        public static string matchingGroup = null;
        public int _roomIdSeed = 0;
        public RoomManager(IBusControl bus, IConfiguration configuration)
        {
            _bus = bus;
            var rabbitCfg = configuration.GetSection("Rabbitmq");
            mqUri = $"rabbitmq://{rabbitCfg["Host"]}/{rabbitCfg["Vhost"]}";
            matchingGroup = configuration["MatchingGroup"];
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

        public void SyncRoom(OneRoomGroup oneGroup, RoomInfo newRoomInfo)
        {
            var findedRoom = oneGroup.FindRoom(newRoomInfo.RoomId);
           
            if (findedRoom == null)
            {
                findedRoom = newRoomInfo;
                oneGroup.CreatRoom(findedRoom);
                if (findedRoom.IsEmpty())
                {
                    oneGroup.AddEmptyRoom(findedRoom);
                }
                else if (findedRoom.IsFull())
                {

                }
                else
                {
                    if (_matchingQueue.TryGetValue(findedRoom.Blind, out var sset))
                    {
                        sset.Add(findedRoom);
                    }
                    else
                    {
                        _matchingQueue.Add(findedRoom.Blind, new SortedSet<RoomInfo>() { findedRoom });
                    }
                }
            }

            else
            {
               
                //从匹配队列和空闲队列中出队列
                if (findedRoom.IsEmpty())
                {
                    oneGroup.RemoveEmptyRoom(findedRoom.Blind, findedRoom.RoomId);
                }
                else if (findedRoom.IsFull())
                {

                }
                else
                {
                    _matchingQueue.TryGetValue(findedRoom.Blind, out var sset);
                    if (sset != null)
                    {
                        sset.Remove(findedRoom);
                    }
                }
                findedRoom.UpdateUserCount(newRoomInfo.UserCount);
                InsertNewInfo(oneGroup, findedRoom);
            }
        }

        public void SyncRooms(string gameKey, string group, List<SyncRoomInfo> syncInfo)
        {
            if (group != MatchingManager.matchingGroup)
            {
                return;
            }
            OneRoomGroup oneGroup = null;
            if (!_roomGroups.TryGetValue(gameKey, out oneGroup))
            {
                oneGroup = new OneRoomGroup();
                _roomGroups.Add(gameKey, oneGroup);
            }
            //先移除不存在的
            RemoveUnExistRoom(oneGroup, syncInfo);
            foreach (var info in syncInfo)
            {
                SyncRoom(oneGroup, new RoomInfo(info.RoomId, info.UserCount, gameKey, info.Blind));
            }
        }

        public void RemoveUnExistRoom(OneRoomGroup oneGroup, List<SyncRoomInfo> existRooms)
        {
            var deletedRooms = oneGroup.RemoveNoExist(existRooms);
            foreach (var room in deletedRooms)
            {
                if (_matchingQueue.TryGetValue(room.Blind, out var matchingRooms))
                {
                    matchingRooms.Remove(room);
                }
            }
        }

        public async Task<RoomInfo> CreateRoom(string gameKey, long blind)
        {
            //生成roomId
            string roomId = $"{matchingGroup}-{++_roomIdSeed}";
            
            if (!_roomConfig.TryGetValue(blind, out var blindConfig))
            {
                throw new Exception($"have no blind{blind} room");
            }

            var busClient = _bus.CreateRequestClient<CreateRoomMqCommand>(new Uri($"{mqUri}/{gameKey}"), TimeSpan.FromSeconds(3));
            
            var busResponse = await busClient.GetResponseExt<CreateRoomMqCommand, BaseResponse>
                (new CreateRoomMqCommand(roomId, gameKey,  blind, blindConfig.MinCoins,
                blindConfig.MaxCoins, blindConfig.TipsPersent, RoomInfo.MAX_USER_NUM, 
                blindConfig.MinCarry, blindConfig.MaxCarry));
            if (busResponse.Message.StatusCode != StatuCodeDefines.Success)
            {
                throw new Exception($"Create Room {roomId} blind{blind} error");
            }
            return new RoomInfo(roomId, 0, gameKey, blind);
        }

        public void  UpdateOneGroup(OneRoomGroup oneGroup, RoomInfo newRoomInfo)
        {
            var findedRoom = oneGroup.FindRoom(newRoomInfo.RoomId);
            if (findedRoom == null)
            {
                throw new Exception($"somthing error cannot find room{newRoomInfo.RoomId}");
            }
            
            //从匹配队列和空闲队列中出队列
            if (findedRoom.IsEmpty())
            {
                oneGroup.RemoveEmptyRoom(findedRoom.Blind, findedRoom.RoomId);
            }
            else if (findedRoom.IsFull())
            {

            }
            else
            {
                _matchingQueue.TryGetValue(findedRoom.Blind, out var sset);
                if (sset != null)
                {
                    sset.Remove(findedRoom);
                }
                
            }
            findedRoom.UpdateUserCount(newRoomInfo.UserCount);
            InsertNewInfo(oneGroup, findedRoom);
        }

        public void UpdateRoom(RoomInfo newRoomInfo)
        {
             OneRoomGroup oneGroup = _roomGroups[newRoomInfo.GameKey];
             UpdateOneGroup(oneGroup, newRoomInfo);
            
        }

        public void InsertNewInfo(OneRoomGroup oneGroup, RoomInfo info)
        {
            if (info.IsEmpty())
            {
                oneGroup.AddEmptyRoom(info);
            }
            else if (info.IsFull())
            {

            }
            else
            {
                if (_matchingQueue.TryGetValue(info.Blind, out var sset))
                {
                    sset.Add(info);
                }
                else
                {
                    _matchingQueue.Add(info.Blind, new SortedSet<RoomInfo>() { info });
                }
            }
        }

        public async Task<RoomInfo> GetRoom(long id, long blind)
        {

            //如果没有任何一个game启动
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
                    oneGroup.Value.CreatRoom(roomInfo);
                }
                return roomInfo;
            }
            else
            {
                roomInfo = matchingRooms.First<RoomInfo>();
                matchingRooms.Remove(roomInfo);
                if (roomInfo.IsFull() || roomInfo.IsEmpty())
                {
                    throw new Exception("matchingRooms matching queue error");
                }
            }
            return roomInfo;
        }

        public async Task<TResponse> SendToGameRoom<TRequest, TResponse>(string gameKey, TRequest request)
            where TRequest : class where TResponse : class
        {
            var busClient = _bus.CreateRequestClient<TRequest>(new Uri($"{mqUri}/{gameKey}"), TimeSpan.FromSeconds(3));
            var response = await busClient.GetResponseExt<TRequest, TResponse>(request);
            return response.Message;
        }

        public void OnUserCountChange(string gameKey, string roomId, long blind, int userCount)
        {
            OneRoomGroup oneGroup = null;
            if (!_roomGroups.TryGetValue(gameKey, out oneGroup))
            {
                oneGroup = new OneRoomGroup();
                _roomGroups.Add(gameKey, oneGroup);
            }
            
            SyncRoom(oneGroup, new RoomInfo(roomId, userCount, gameKey, blind));
        }
    }
}
