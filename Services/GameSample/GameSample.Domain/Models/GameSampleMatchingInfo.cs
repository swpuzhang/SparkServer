using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSample.Domain.Models
{
                 
    public class GameSampleMatchingResponseInfo : UserEntity
    {

        public GameSampleMatchingResponseInfo()
        {

        }

        public GameSampleMatchingResponseInfo(long id, string roomId, long blind, string gameKey)
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
