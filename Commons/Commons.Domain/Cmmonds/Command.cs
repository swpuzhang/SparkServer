using Commons.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Domain.Cmmonds
{
    public abstract class Command : Message
    {
        public DateTime TimeStamp { get; private set; }
        //public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            TimeStamp = DateTime.Now;
        }
        //public abstract bool IsValid();
    }
}
