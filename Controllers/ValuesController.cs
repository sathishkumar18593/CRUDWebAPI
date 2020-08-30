using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUD.Models;
using System.Web.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Net;
using System.Globalization;
using CRUD.DataLayer;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    public class ValuesController : ControllerBase

    {
        public static SqlConnection cnn;
        public static SqlCommand command;
        public static SqlDataReader dataReader;
        public static string connectionString = "Data Source=SQLD12.tmm.na.corp.toyota.com,8025;Initial Catalog=Workout;Integrated Security=true;";
        public static string query = "";


       // private readonly DataLayer _data;

        // GET api/values
        [HttpGet]
        [EnableCors("_myAllowSpecificOrigins")]
        public List<Employee> Getemployee()
        {


            List<Employee> data = new List<Employee>();
            string query = "Select EmpID,FirstName,LastName,Gender,Age,format(joineddate,'yyyy/MM/dd') as joineddate  from Employee";
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            command = new SqlCommand(query, cnn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                data.Add(new Employee()
                {
                    id = dataReader.GetValue(0) != DBNull.Value ? Convert.ToInt32(dataReader.GetValue(0)) : (int?)null,
                    Firstname = dataReader.GetValue(1) != DBNull.Value ? dataReader.GetValue(1).ToString() : "",
                    LastName = dataReader.GetValue(2) != DBNull.Value ? dataReader.GetValue(2).ToString() : "",
                    Gender = dataReader.GetValue(3) != DBNull.Value ? dataReader.GetValue(3).ToString() : "",
                    Age = dataReader.GetValue(4) != DBNull.Value ? Convert.ToInt32(dataReader.GetValue(4)) : (int?)null,
                    Joineddate = dataReader.GetValue(5) != DBNull.Value ? String.Format("{0:MM/dd/yyyy}", dataReader.GetValue(5).ToString())  : "",
                    View = "View",
                    Edit = "Edit",
                    Delete = "Delete"
                });
            }
            cnn.Close();
            return data;

        }

        // GET api/values/5
        [HttpGet("{id}")]
        [EnableCors("_myAllowSpecificOrigins")]
        public Employee Get(int id)
        {
            Employee data = new Employee();
            string query = "Select EmpID,FirstName,LastName,Gender,Age,format(joineddate,'yyyy/MM/dd') as joineddate  from Employee where EmpId = " + id+"";
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            command = new SqlCommand(query, cnn);
            dataReader = command.ExecuteReader();
            
            while (dataReader.Read())
            {
                data = (new Employee()
                {
                    id = dataReader.GetValue(0) != DBNull.Value ? Convert.ToInt32(dataReader.GetValue(0)) : (int?)null,
                    Firstname = dataReader.GetValue(1) != DBNull.Value ? dataReader.GetValue(1).ToString() : "",
                    LastName = dataReader.GetValue(2) != DBNull.Value ? dataReader.GetValue(2).ToString() : "",
                    Gender = dataReader.GetValue(3) != DBNull.Value ? dataReader.GetValue(3).ToString() : "",
                    Age = dataReader.GetValue(4) != DBNull.Value ? Convert.ToInt32(dataReader.GetValue(4)) : (int?)null,
                    Joineddate = dataReader.GetValue(5) != DBNull.Value ? String.Format("{0:MM/dd/yy}", dataReader.GetValue(5).ToString()) : ""
                });
            }
            cnn.Close();
            return data;
        }

        // POST api/values
        [HttpPost]
        [EnableCors("_myAllowSpecificOrigins")]
        public void Post(Employee data)
        {
            string query = "Insert into Employee values('"+data.Firstname+"','"+data.LastName+"','"+data.Gender+"',"+data.Age+",'"+ data.Joineddate + "')";
            //string date = data.Joineddate.ToString("MM/dd/yyyy");
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            command = new SqlCommand(query, cnn);
            command.ExecuteNonQuery();
            cnn.Close();
        }

        // PUT api/values
        [HttpPut]
        [EnableCors("_myAllowSpecificOrigins")]
        public int Put(Employee data)
        {
            string query = "update employee set firstname = '" + data.Firstname + "', lastname = '" + data.LastName + "', gender = '" + data.Gender + "',age = " + data.Age + ",joineddate = '" + data.Joineddate + "' where empid = "+data.id+"";
            //string date = data.Joineddate.ToString("MM/dd/yyyy");
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            command = new SqlCommand(query, cnn);
            command.ExecuteNonQuery();
            cnn.Close();
            return 1;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            string query = "Delete from Employee where EmpId = " + id + "";
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            command = new SqlCommand(query, cnn);
            command.ExecuteNonQuery();
            cnn.Close();
            return 1;
        }
    }
}
