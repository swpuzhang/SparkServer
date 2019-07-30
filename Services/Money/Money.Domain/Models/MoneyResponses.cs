using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Domain.Models
{
    public class MoneyResponse
    {

        private MoneyResponse()
        {

        }

        public MoneyResponse(long id)
        {
            Id = id;
        }

        public Int64 Id { get; set; } 
    }

}
