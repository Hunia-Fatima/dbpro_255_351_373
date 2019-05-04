using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Mobile_Info.Models;

namespace Mobile_Info.Controllers
{
    public class HomeController : Controller
    {
        String ConStr = "Data Source=DESKTOP-H2EOT5V\\SQLEXPRESS;Initial Catalog=DB20;Integrated Security=True";

        // GET: Home
        public ActionResult Index()
        {
            SqlConnection connection = new SqlConnection(ConStr);
             connection.Open();
            SqlConnection connection2 = new SqlConnection(ConStr);
            connection2.Open();
            String query = "SELECT * from Mobile";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader rdr = cmd.ExecuteReader();
            int imgID = 0;
            
            List<MobileViewModel> mobiles = new List<MobileViewModel>();
            while (rdr.Read())
            {
                MobileViewModel mobile = new MobileViewModel();
                int cat = Convert.ToInt32(rdr.GetValue(3));

                //Finding the category against the ID of the given mobile

                query = "SELECT * FROM Category WHERE Id = '" + cat + "'";
                cmd = new SqlCommand(query, connection2);
                SqlDataReader rdr2 = cmd.ExecuteReader();
                String category = "";
                while (rdr2.Read())
                {
                    category = (rdr2.GetValue(1)).ToString();
                }
                rdr2.Close();

                //Finding the OS against the operating System ID of the given mobile

                int os = Convert.ToInt32(rdr.GetValue(1));
                query = "SELECT * FROM LookUp WHERE Id = '" + os + "'";
                cmd = new SqlCommand(query, connection2);
                rdr2 = cmd.ExecuteReader();
                String operatingSystem = "";
                while (rdr2.Read())
                {
                    operatingSystem = (rdr2.GetValue(1)).ToString();
                }
                rdr2.Close();

                //Finding the Color against the Color ID of the given mobile

                int col = Convert.ToInt32(rdr.GetValue(2));
                query = "SELECT * FROM Category WHERE Id = '" + col + "'";
                cmd = new SqlCommand(query, connection2);
                rdr2 = cmd.ExecuteReader();
                String color = "";
                while (rdr2.Read())
                {
                    color = (rdr2.GetValue(1)).ToString();
                }
                rdr2.Close();
                imgID = rdr.GetInt32(0);
                mobile.Title = rdr.GetString(13);
                mobile.Id = rdr.GetInt32(0);
                mobile.Category = category;
                mobile.OS = operatingSystem;
                mobile.Color = color;
                mobile.Dimensions = rdr.GetDouble(4);
                mobile.Weight = rdr.GetDouble(5);
                mobile.Display = rdr.GetDouble(6);
                mobile.Memory = rdr.GetInt32(7);
                mobile.RAM = rdr.GetInt32(8);
                mobile.FrontCameraPx = rdr.GetInt32(9);
                mobile.BackCameraPx = rdr.GetInt32(10);
                if (rdr.GetInt32(11) == 3)
                {
                    mobile.Networks = "3G";
                }
                else
                {
                    mobile.Networks = "4G";
                }
                mobile.Price = rdr.GetInt32(12);
                mobiles.Add(mobile);
            }
            query = "SELECT * FROM Images WHERE id = '" + imgID + "'";
            cmd = new SqlCommand(query, connection2);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                byte[] image = rdr.GetSqlBytes(24).Value;
                ViewBag.Image1 = "data:image/png;base64," + Convert.ToBase64String(image, 0, image.Length);
                
            }
            rdr.Close();
            
            ViewBag.mobiles = mobiles;
            return View();
        }
        public ActionResult Details()
        {
            int id = Convert.ToInt32(RouteData.Values["id"]);
            ViewBag.Id = id;
            SqlConnection connection = new SqlConnection(ConStr);
            connection.Open();
            SqlConnection connection2 = new SqlConnection(ConStr);
            connection2.Open();
            String query = "SELECT * from Mobile WHERE Id = '" + id + "'";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader rdr = cmd.ExecuteReader();
            MobileViewModel mobile = new MobileViewModel();
            while (rdr.Read())
            {
                int cat = Convert.ToInt32(rdr.GetValue(3));

                //Finding the category against the ID of the given mobile

                query = "SELECT * FROM Category WHERE Id = '" + cat + "'";
                cmd = new SqlCommand(query, connection2);
                SqlDataReader rdr2 = cmd.ExecuteReader();
                String category = "";
                while (rdr2.Read())
                {
                    category = (rdr2.GetValue(1)).ToString();
                }
                rdr2.Close();

                //Finding the OS against the operating System ID of the given mobile

                int os = Convert.ToInt32(rdr.GetValue(1));
                query = "SELECT * FROM LookUp WHERE Id = '" + os + "'";
                cmd = new SqlCommand(query, connection2);
                rdr2 = cmd.ExecuteReader();
                String operatingSystem = "";
                while (rdr2.Read())
                {
                    operatingSystem = (rdr2.GetValue(1)).ToString();
                }
                rdr2.Close();

                //Finding the Color against the Color ID of the given mobile

                int col = Convert.ToInt32(rdr.GetValue(2));
                query = "SELECT * FROM Category WHERE Id = '" + col + "'";
                cmd = new SqlCommand(query, connection2);
                rdr2 = cmd.ExecuteReader();
                String color = "";
                while (rdr2.Read())
                {
                    color = (rdr2.GetValue(1)).ToString();
                }
                rdr2.Close();
                mobile.Title = rdr.GetString(13);
                mobile.Id = rdr.GetInt32(0);
                mobile.Category = category;
                mobile.OS = operatingSystem;
                mobile.Color = color;
                mobile.Dimensions = rdr.GetDouble(4);
                mobile.Weight = rdr.GetDouble(5);
                mobile.Display = rdr.GetDouble(6);
                mobile.Memory = rdr.GetInt32(7);
                mobile.RAM = rdr.GetInt32(8);
                mobile.FrontCameraPx = rdr.GetInt32(9);
                mobile.BackCameraPx = rdr.GetInt32(10);
                if (rdr.GetInt32(11) == 3)
                {
                    mobile.Networks = "3G";
                }
                else
                {
                    mobile.Networks = "4G";
                }
                mobile.Price = rdr.GetInt32(12);
            }
            int imgID = 0;
            ViewBag.mobileDetail = mobile;
            query = "SELECT * FROM Images WHERE id = '" + imgID + "'";
            cmd = new SqlCommand(query, connection2);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                byte[] image = rdr.GetSqlBytes(24).Value;
                ViewBag.Image1 = "data:image/png;base64," + Convert.ToBase64String(image, 0, image.Length);

            }
            rdr.Close();
            return View();
        }
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(AdminViewModel admin)
        {
            if(admin.Email == "admin@mobileInfo.com" && admin.Password == "1234567")
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
                rdr.Close();
                ViewBag.OS = os;
                query = "UPDATE Administrator SET IsActive = 1";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                return View("../Admin/AddMobile");
            }
            else
            {
                return View();
            }
            
        }
    }
}