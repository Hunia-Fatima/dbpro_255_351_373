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
            SqlConnection connection = new SqlConnection(ConStr);
            connection.Open();
            String query = "SELECT * FROM Category";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader rdr = cmd.ExecuteReader();
            List<String> categories = new List<string>();
            while (rdr.Read())
            {
                categories.Add(rdr.GetValue(1).ToString());
            }
            ViewBag.Categories = categories;
            rdr.Close();
            query = "SELECT * FROM Lookup WHERE Category = 'Color'";
            cmd = new SqlCommand(query, connection);
            rdr = cmd.ExecuteReader();
            List<String> colors = new List<string>();
            while (rdr.Read())
            {
                colors.Add(rdr.GetValue(1).ToString());
            }
            ViewBag.Colors = colors;
            rdr.Close();
            query = "SELECT * FROM Lookup WHERE Category = 'OS'";
            cmd = new SqlCommand(query, connection);
            rdr = cmd.ExecuteReader();
            List<String> os = new List<string>();
            while (rdr.Read())
            {
                os.Add(rdr.GetValue(1).ToString());
            }
            ViewBag.OS = os;
            return View();
        }
        [HttpPost]
        public ActionResult AddMobile(MobileViewModel mobile)
        {
            SqlConnection connection = new SqlConnection(ConStr);
            connection.Open();
            String OS = mobile.OS;
            String title = mobile.Title;
            String Cat = mobile.Category;
            String Col = mobile.Color;
            double Weight = mobile.Weight;
            double Dim = mobile.Dimensions;
            double dis = mobile.Display;
            int mem = mobile.Memory;
            int price = mobile.Price;
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
            String query = "SELECT * FROM LookUp WHERE Value = '" + OS + "'";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader rdr = cmd.ExecuteReader();
            int operatingSystem = 0;
            while (rdr.Read())
            {
                operatingSystem = Convert.ToInt32(rdr.GetValue(0));
            }
            rdr.Close();
            query = "SELECT * FROM Category WHERE Title = '" + Cat + "'";
            cmd = new SqlCommand(query, connection);
            rdr = cmd.ExecuteReader();
            int category = 0;
            while (rdr.Read())
            {
                category = Convert.ToInt32(rdr.GetValue(0));
            }
            rdr.Close();
            query = "SELECT * FROM LookUp WHERE Value = '" + Col + "'";
            cmd = new SqlCommand(query, connection);
            rdr = cmd.ExecuteReader();
            int color = 0;
            while (rdr.Read())
            {
                color = Convert.ToInt32(rdr.GetValue(0));
            }
            rdr.Close();
            query = "INSERT INTO Mobile(OS, Color, Category, Dimensions, Weight, Display, Memory, RAM, FrontCamerPx, BackCamerPx, Networks, Price, Title) VALUES('" + operatingSystem + "', '" + color + "', '" + category + "', '" + Dim + "', '" + Weight + "', '" + dis + "', '" + mem + "', '" + ram + "', '" + front + "', '" + back + "', '" + Network + "', '" + price + "','" + title + "')";
            cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            query = "SELECT * FROM Category";
            cmd = new SqlCommand(query, connection);
            rdr = cmd.ExecuteReader();
            List<String> categories = new List<string>();
            while (rdr.Read())
            {
                categories.Add(rdr.GetValue(1).ToString());
            }
            ViewBag.Categories = categories;
            rdr.Close();
            query = "SELECT * FROM Lookup WHERE Category = 'Color'";
            cmd = new SqlCommand(query, connection);
            rdr = cmd.ExecuteReader();
            List<String> colors = new List<string>();
            while (rdr.Read())
            {
                colors.Add(rdr.GetValue(1).ToString());
            }
            ViewBag.Colors = colors;
            rdr.Close();
            query = "SELECT * FROM Lookup WHERE Category = 'OS'";
            cmd = new SqlCommand(query, connection);
            rdr = cmd.ExecuteReader();
            List<String> os = new List<string>();
            while (rdr.Read())
            {
                os.Add(rdr.GetValue(1).ToString());
            }
            ViewBag.OS = os;
            return View();
        }
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(CategoryViewModel category)
        {
            SqlConnection connection = new SqlConnection(ConStr);
            connection.Open();
            List<String> list = new List<string>();
            list.Add("Samsung");
            list.Add("OPPO");
            ViewBag.listt = list;
            String query = "INSERT INTO Category(Title) VALUES('" + category.Title + "')";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            return View();
        }
    }
}