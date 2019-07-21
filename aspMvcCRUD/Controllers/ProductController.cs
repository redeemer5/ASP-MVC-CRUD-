using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using aspMvcCRUD.Models;

namespace aspMvcCRUD.Controllers
{
    public class ProductController : Controller
    {

        // variables
        string conectionString = "Data Source=DESKTOP-SD2P6TG\\SQLEXPRESS;Initial Catalog=productMvc;Integrated Security=True";


        // GET: Product
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conectionString))
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select * from productTbl", con);
                sda.Fill(dt);

            }
            return View(dt);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProductModel());
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel productModel)
        {
            try
            {
                 using(SqlConnection con = new SqlConnection(conectionString))
                {
                    con.Open();
                    String query = "insert into productTbl values(@productName,@price,@count)";
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@productName", productModel.productName);
                    command.Parameters.AddWithValue("@price", productModel.price);
                    command.Parameters.AddWithValue("@count", productModel.count);
                    command.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProductModel productModel = new ProductModel();
            DataTable dt = new DataTable();
            using(SqlConnection con = new SqlConnection(conectionString))
            {
                con.Open();
                string query = "select * from productTbl where productid = @productid";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.SelectCommand.Parameters.AddWithValue("@productid", id);
                sda.Fill(dt);

                if(dt.Rows.Count == 1)
                {
                    productModel.productid = Convert.ToInt32(dt.Rows[0][0].ToString());
                    productModel.productName = dt.Rows[0][1].ToString();
                    productModel.price = Convert.ToDecimal(dt.Rows[0][2].ToString());
                    productModel.count = Convert.ToInt32(dt.Rows[0][3].ToString());

                    return View(productModel);
                }
                else
                {
                    return RedirectToAction("index");
                }
            }
            
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel productModel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conectionString))
                {
                    con.Open();
                    String query = "update productTbl set productName =  @productName, price = @price, count = @count where productid = @productid";
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@productid", productModel.productid);
                    command.Parameters.AddWithValue("@productName", productModel.productName);
                    command.Parameters.AddWithValue("@price", productModel.price);
                    command.Parameters.AddWithValue("@count", productModel.count);
                    command.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(conectionString))
            {
                con.Open();
                String query = "delete from productTbl where productid = @productid";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@productid", id);
                command.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
