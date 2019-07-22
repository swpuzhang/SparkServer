using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Domain.Models
{
    public abstract class UserEntity
    {

        [BsonId]
        public virtual Int64 Id
        {
            get;

            protected set;
            
        }

        public bool IsTransient()
        {
            return this.Id == default(Int64);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is UserEntity))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            UserEntity item = obj as UserEntity;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                return this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)
            }
            else
                return base.GetHashCode();

        }
        public static bool operator ==(UserEntity left, UserEntity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(UserEntity left, UserEntity right)
        {
            return !(left == right);
        }

    }
}
