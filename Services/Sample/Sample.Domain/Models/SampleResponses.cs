using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Domain.Models
{
    public class SampleResponse
    {

        private SampleResponse()
        {

        }

        public SampleResponse(long id)
        {
            Id = id;
        }

        public Int64 Id { get; set; } 
    }

}
