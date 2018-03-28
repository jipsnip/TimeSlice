using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TimeSlice.Models;
using TimeSlice.ViewModels;

namespace TimeSlice.Services
{
    public class IUserData
    {

        public IUserData()
        {

        }

        public IEnumerable<User> GetAll()
        {
            //SqlConnection con = new SqlConnection("data source=den1.mssql1.gear.host;initial catalog = timeslice;user id=timeslice;password=Password123!");
            //SqlCommand comm = new SqlCommand();
            //var _query = "SELECT * FROM USERS";
            //comm.Connection = con;
            //comm.CommandType = CommandType.Text;
            //comm.CommandText = _query;

            //con.Open();
            //comm.ExecuteNonQuery();
            //SqlDataReader reader = comm.ExecuteReader();
            //List<User> users = new List<User>();
            //if (reader.HasRows)
            //{
            //    while (reader.Read())
            //    {
            //        users.Append(new User(reader.GetInt32(0), 
            //                              reader.GetValue(3).ToString(), 
            //                              reader.GetValue(4).ToString(), 
            //                              reader.GetValue(1).ToString(), 
            //                              reader.GetValue(2).ToString(), 
            //                              reader.GetInt32(5)));
            //    }
            //}
            //return users;
            return new List<User>();
        }

        public bool Verify(UserLoginModel user)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            return BCrypt.Net.BCrypt.Verify(user.Password, hashedPassword);
        }

        public void Signup()
        {

        }

    }
}
