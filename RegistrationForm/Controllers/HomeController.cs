using RegistrationForm.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegistrationForm.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
            string sql = "SELECT * FROM Users";
            List<UserDetails> users = new List<UserDetails>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {
                    DataTable dataTable = new DataTable();
                    connection.Open();
                    adapter.Fill(dataTable);
                    connection.Close();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        UserDetails userdet = new UserDetails();
                        userdet.Name = row["name"].ToString().Trim();
                        userdet.age = Convert.ToInt32(row["age"]);
                        userdet.Gender = row["gender"].ToString().Trim();
                        userdet.Email = row["email"].ToString().Trim();
                        userdet.PhoneNumber = row["phnum"].ToString().Trim();
                        userdet.Country = row["country"].ToString().Trim();
                        userdet.state = row["state"].ToString().Trim();
                        
                        users.Add(userdet);
                    }
                }
            }
            return View(users);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string InsertUser(string name, int age, string gender, string email, string phnum, string country, string state)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO Users (name,age,gender,email,phnum,country,state) VALUES(@name,@age,@gender,@email,@phnum,@country,@state)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@age", age);
                    command.Parameters.AddWithValue("@gender", gender);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@phnum", phnum);
                    command.Parameters.AddWithValue("@country", country);
                    command.Parameters.AddWithValue("@state", state);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
            }

        }


    }
}