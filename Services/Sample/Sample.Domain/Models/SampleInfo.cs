using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Domain.Models
{
                 
    public class SampleInfo : UserEntity
    {

       

        public SampleInfo()
        {

        }

        public SampleInfo(long id)
        {
            Id = id;
        }


        public override bool Equals(object obj)
        {
            return obj is SampleInfo info &&
                   base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
