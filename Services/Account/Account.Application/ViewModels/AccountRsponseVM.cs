using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.ViewModels
{
    public class AccounResponsetVM
    {

        public Int64 Id { get; set; }
        public string PlatformAccount { get; set; }
        public string UserName { get; set; }
        public int Sex { get; set; }
        public string HeadUrl { get; set; }

        public string token { get; set; }

    }
}
