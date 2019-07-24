using Commons.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Models
{
    public class UserIdGenInfo : UserEntity
    {
        public long UserId { get; private set; }
    }
}
