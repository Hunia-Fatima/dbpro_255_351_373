using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace Mobile_Info.Controllers
{
    public class HomeController : Controller
    {
        String ConStr = "Data Source=HN;Initial Catalog=DB20;Integrated Security=True";

        // GET: Home
        public ActionResult Index()
        {
            SqlConnection connection = new SqlConnection(ConStr);
            connection.Open();
            String query = "SELECT * from Lookup Where id = 2";
            SqlCommand cmd = new SqlCommand(query,connection);
            SqlDataReader rdr = cmd.ExecuteReader();
            String Output = "nooooooooooooooooooooooooooooooooooo";
            while (rdr.Read())
            {
                Output = rdr.GetValue(0).ToString();
            }
            ViewBag.Output = Output;
            return View();
        }
    }
}