using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Domain.Models
{
    public class LoginCheckInfo
    {
        public LoginCheckInfo()
        {
        }

        public LoginCheckInfo(long id)
        {
            Id = id;
        }

        public Int64 Id { get; private set; }
    }
}
