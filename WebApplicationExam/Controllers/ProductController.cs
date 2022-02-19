using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationExam.Models;

namespace WebApplicationExam.Controllers
{
    public class ProductController : Controller
    {
        string ConnectionString = "";
        SqlConnection connection = new SqlConnection();

        public ProductController()
        {
            ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ExamDb;Integrated Security=True;Connect Timeout=30;";
            connection.ConnectionString = ConnectionString;
        }
        // GET: Product
        public ActionResult Index()
        {

            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = "productlist";
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            List<product> productlist = new List<product>();
            while (dataReader.Read())
            {
                product model = new product();
                model.ProductId = int.Parse(dataReader["ProdctId"].ToString());
                model.ProductName = dataReader["productname"].ToString();
                model.Rate = double.Parse(dataReader["rate"].ToString());
                model.Description = dataReader["description"].ToString();
                model.CategoryName = dataReader["categoryname"].ToString();
                productlist.Add(model);
            }
            return View(productlist);
        }


        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            product model = new product();
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = "getSingleproduct";
            sqlCommand.Parameters.AddWithValue("@productid", SqlDbType.Int).Value = id;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {

                model.ProductId = int.Parse(sqlDataReader["ProdctId"].ToString());
                model.ProductName = sqlDataReader["productname"].ToString();
                model.Rate = double.Parse(sqlDataReader["rate"].ToString());
                model.Description = sqlDataReader["description"].ToString();
                model.CategoryName = sqlDataReader["categoryname"].ToString();

            }
            return View(model);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandText = "updateproduct";
                sqlCommand.Parameters.AddWithValue("@productid", SqlDbType.Int).Value = id;
                sqlCommand.Parameters.AddWithValue("@productname", SqlDbType.VarChar).Value = collection["ProductName"].ToString();
                sqlCommand.Parameters.AddWithValue("@description", SqlDbType.VarChar).Value = collection["Description"].ToString();
                sqlCommand.Parameters.AddWithValue("@rate", SqlDbType.Decimal).Value = collection["Rate"].ToString();
                sqlCommand.Parameters.AddWithValue("@categoryname", SqlDbType.VarChar).Value = collection["CategoryName"].ToString();
                sqlCommand.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}
