using System;
using System.Collections.Generic;
using System.Text;

namespace Sangong.Domain.Models
{
    public class SangongResponse
    {

        private SangongResponse()
        {

        }

        public SangongResponse(long id)
        {
            Id = id;
        }

        public Int64 Id { get; set; } 
    }

}
