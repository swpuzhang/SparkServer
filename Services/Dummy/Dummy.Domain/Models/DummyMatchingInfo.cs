using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dummy.Domain.Models
{
                 
    public class DummyMatchingResponseInfo : UserEntity
    {

        public DummyMatchingResponseInfo()
        {

        }

        public DummyMatchingResponseInfo(long id, string roomId, long blind, string gameKey)
        {
            Id = id;
            RoomId = roomId;
            Blind = blind;
            GameKey = gameKey;
        }

        public string RoomId { get; private set; }
        public long Blind { get; private set; }
        public string GameKey { get; private set; }
    }
}
