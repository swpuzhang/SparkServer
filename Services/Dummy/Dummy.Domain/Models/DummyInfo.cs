using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dummy.Domain.Models
{
                 
    public class DummyInfo : UserEntity
    {

       

        public DummyInfo()
        {

        }

        public DummyInfo(long id)
        {
            Id = id;
        }


        public override bool Equals(object obj)
        {
            return obj is DummyInfo info &&
                   base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
