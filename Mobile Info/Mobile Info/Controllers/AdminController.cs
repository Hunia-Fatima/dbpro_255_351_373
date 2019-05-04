using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mobile_Info.Models;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Data;
using System.Configuration;


namespace Mobile_Info.Controllers
{
    public class AdminController : Controller
    {
        String ConStr = "Data Source=DESKTOP-H2EOT5V\\SQLEXPRESS;Initial Catalog=DB20;Integrated Security=True";
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
            rdr.Close();
            ViewBag.OS = os;

            
            //Image Code
            if (mobile.ImageFileS != null && mobile.ImageFileF != null && mobile.ImageFileB != null)
            {
                string pic1 = System.IO.Path.GetFileName(mobile.ImageFileS.FileName);
                string pic2 = System.IO.Path.GetFileName(mobile.ImageFileF.FileName);
                string pic3 = System.IO.Path.GetFileName(mobile.ImageFileB.FileName);

                string path1 = System.IO.Path.Combine(
                                       Server.MapPath("~/images/profile"), pic1);
                string path2 = System.IO.Path.Combine(
                                      Server.MapPath("~/images/profile"), pic2);
                string path3 = System.IO.Path.Combine(
                                      Server.MapPath("~/images/profile"), pic3);

                // file is uploaded
                //mobile.ImageFile.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB


                //Passing parameters to query
                SqlConnection connection1 = new SqlConnection(ConStr);
                connection1.Open();
                using (MemoryStream ms = new MemoryStream())
                {
                    mobile.ImageFileS.InputStream.CopyTo(ms);
                    byte[] array1 = ms.GetBuffer();
                    ViewBag.Image1 = array1;

                    mobile.ImageFileF.InputStream.CopyTo(ms);
                    byte[] array2 = ms.GetBuffer();
                    ViewBag.Image2 = array2;

                    mobile.ImageFileB.InputStream.CopyTo(ms);
                    byte[] array3 = ms.GetBuffer();
                    ViewBag.Image3 = array3;


                    String query1 = "SELECT Id from Mobile where OS='" + operatingSystem + "' AND Color='" + color + "' AND Category='" + category + "' AND Dimensions='" + Dim + "' AND Weight='" + Weight + "' AND Display='" + dis + "' AND Memory='" + mem + "' AND RAM='" + ram + "' AND FrontCamerPx='" + front + "' AND BackCamerPx='" + back + "' AND Networks='" + Network + "' AND Price='" + price + "' AND Title='" + title + "'";
                    cmd = new SqlCommand(query1, connection);
                    rdr = cmd.ExecuteReader();
                    int mobid = 0;
                    
                    while (rdr.Read())
                    {
                        mobid = Convert.ToInt32(rdr.GetValue(0));
                    }
                    rdr.Close();
                    string query2 = "INSERT INTO Images(MobileId, SideViewImage,FrontViewImage,BackViewImage) VALUES ('" + mobid + "','" + array1 + "','" + array2 + "','" + array3 + "')";

                    SqlCommand cmd2 = new SqlCommand(query2, connection1);
                    cmd2.ExecuteNonQuery();
                    connection1.Close();
                }

            }
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