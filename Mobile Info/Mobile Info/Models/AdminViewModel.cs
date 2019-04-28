using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mobile_Info.Models
{

    public class AdminViewModel
    {
        public String Email { get; set; }
        public String Password { get; set; } 
        public bool isActive { get; set; }
    }
}