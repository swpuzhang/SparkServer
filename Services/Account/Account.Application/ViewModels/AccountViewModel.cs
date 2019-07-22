using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Account.Application.ViewModels
{
    
    public class AccountVM
    {

        public Int64 Id { get;  set; }
        public string PlatformAccount { get;  set; }
        public string UserName { get;  set; }
        public int Sex { get;  set; }
        public string HeadUrl { get;  set; }
        
    }
}
