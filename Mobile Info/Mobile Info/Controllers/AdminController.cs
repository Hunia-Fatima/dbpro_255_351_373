using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mobile_Info.Models;
using System.Data.SqlClient;

namespace Mobile_Info.Controllers
{
    public class AdminController : Controller
    {
        String ConStr = "Data Source=HN;Initial Catalog=DB20;Integrated Security=True";
        // GET: Admin
        public ActionResult AddMobile()
        {
            List<String> list = new List<string>();
            list.Add("Samsung");
            list.Add("OPPO");
            ViewBag.listt = list;
            return View();
        }
        [HttpPost]
        public ActionResult AddMobile(MobileViewModel mobile)
        {
            SqlConnection connection = new SqlConnection(ConStr);
            connection.Open();
            String OS = mobile.OS;
            String Cat = mobile.Category;
            float Weight = mobile.Weight;
            float Dim = mobile.Dimensions;
            float dis = mobile.Display;
            int mem = mobile.Memeory;
            int ram = mobile.RAM;
            int front = mobile.FrontCameraPx;
            int back = mobile.BackCameraPx;
            int Network;
            if(mobile.Networks == "3G")
            {
                Network = 3;
            }
            else
            {
                Network = 4;
            }
            String query = "INSERT INTO Mobile(OS, Color, Category, Dimensions, Weight, Display, Memory, RAM, FrontCamerPx, BackCamerPx, Networks) VALUES(1, 4, 1, '" + Dim + "', '" + Weight + "', '" + dis + "', '" + mem + "', '" + ram + "', '" + front + "', '" + back + "', '" + Network + "')";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            ViewBag.Name = mobile.Category;
            return View();
        }
        public ActionResult AddCategory()
        {
            List<String> list = new List<string>();
            list.Add("Samsung");
            list.Add("OPPO");
            ViewBag.listt = list;
            return View();
        }
    }
}