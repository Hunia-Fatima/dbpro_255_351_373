using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mobile_Info.Models
{
    public class MobileViewModel
    {
        public String Title { get; set; }
        public int Id { get; set; }
        public String Category { get; set; }
        public String Color { get; set; }
        public String OS { get; set; }
        public double Dimensions { get; set; }
        public double Weight { get; set; }
        public double Display { get; set; }
        public int Memory { get; set; }
        public int RAM { get; set; }
        public int FrontCameraPx { get; set; }
        public int BackCameraPx { get; set; }
        public int Price { get; set; }
        public String Networks { get; set; }
        public string ImagePathS { get; set; }
        public HttpPostedFileBase ImageFileS { get; set; }
        public string ImagePathF { get; set; }
        public HttpPostedFileBase ImageFileF { get; set; }
        public string ImagePathB { get; set; }
        public HttpPostedFileBase ImageFileB { get; set; }
    }

}