using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mobile_Info.Models
{
    public class MobileViewModel
    {
        public String Category { get; set; }
        public String Color { get; set; }
        public String OS { get; set; }
        public float Dimensions { get; set; }
        public float Weight { get; set; }
        public float Display { get; set; }
        public int Memeory { get; set; }
        public int RAM { get; set; }
        public int FrontCameraPx { get; set; }
        public int BackCameraPx { get; set; }
        public String Networks { get; set; }
    }

}