using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSample.Domain.Models
{
                 
    public class GameSampleInfo : UserEntity
    {

       

        public GameSampleInfo()
        {

        }

        public GameSampleInfo(long id)
        {
            Id = id;
        }


        public override bool Equals(object obj)
        {
            return obj is GameSampleInfo info &&
                   base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
