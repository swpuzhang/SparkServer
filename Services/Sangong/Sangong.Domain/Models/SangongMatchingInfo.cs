using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sangong.Domain.Models
{
                 
    public class SangongMatchingResponseInfo : UserEntity
    {

        public SangongMatchingResponseInfo()
        {

        }

        public SangongMatchingResponseInfo(long id, int roomId, long blind, string gameKey)
        {
            Id = id;
            RoomId = roomId;
            Blind = blind;
            GameKey = gameKey;
        }

        public int RoomId { get; private set; }
        public long Blind { get; private set; }
        public string GameKey { get; private set; }
    }
}
