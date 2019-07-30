using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sangong.Domain.Models
{
                 
    public class SangongInfo : UserEntity
    {

       

        public SangongInfo()
        {

        }

        public SangongInfo(long id)
        {
            Id = id;
        }


        public override bool Equals(object obj)
        {
            return obj is SangongInfo info &&
                   base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
