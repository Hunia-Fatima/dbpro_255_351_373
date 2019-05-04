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
        String ConStr = "Data Source=.;Initial Catalog=aeni;Integrated Security=SSPI";
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
            ViewBag.mobiles = mobiles;
            return View();
        }
        private void GetViewData()
        {
            string CS = color.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlDataAdapter da = new SqlDataAdapter("Select * from Mobile", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int columnsCount = AddMobi.HeaderRow.Cells.Count;
            
            PdfPTable pdfTable = new PdfPTable(columnsCount);

            // Loop thru each cell in GrdiView header row
            foreach (TableCell gridViewHeaderCell in GridView1.HeaderRow.Cells)
            {
                // Create the Font Object for PDF document
                Font font = new Font();
                // Set the font color to GridView header row font color
                font.Color = new BaseColor(GridView1.HeaderStyle.ForeColor);

                // Create the PDF cell, specifying the text and font
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text, font));

                // Set the PDF cell backgroundcolor to GridView header row BackgroundColor color
                pdfCell.BackgroundColor = new BaseColor(GridView1.HeaderStyle.BackColor);

                // Add the cell to PDF table
                pdfTable.AddCell(pdfCell);
            }

            // Loop thru each datarow in GrdiView
            foreach (GridViewRow gridViewRow in GridView1.Rows)
            {
                if (gridViewRow.RowType == DataControlRowType.DataRow)
                {
                    // Loop thru each cell in GrdiView data row
                    foreach (TableCell gridViewCell in gridViewRow.Cells)
                    {
                        Font font = new Font();
                        font.Color = new BaseColor(GridView1.RowStyle.ForeColor);

                        PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewCell.Text, font));

                        pdfCell.BackgroundColor = new BaseColor(GridView1.RowStyle.BackColor);

                        pdfTable.AddCell(pdfCell);
                    }
                }
            }

            // Create the PDF document specifying page size and margins
            Document pdfDocument = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            
            PdfWriter.GetInstance(pdfDocument, Response.OutputStream);

            pdfDocument.Open();
            pdfDocument.Add(pdfTable);
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AppendHeader("content-disposition",
                "attachment;filename=Employees.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
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
            ViewBag.mobileDetail = mobile;
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