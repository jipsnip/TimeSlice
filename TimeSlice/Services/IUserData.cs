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

        public int FindRole(string Username)
        {
            SqlConnection con = new SqlConnection("data source=den1.mssql1.gear.host;initial catalog = timeslice;user id=timeslice;password=Password123!");
            SqlCommand comm = new SqlCommand();
            var _query = "SELECT roleId FROM USERS WHERE userName=@username";
            comm.Parameters.AddWithValue("username", Username);
            comm.Connection = con;
            comm.CommandType = CommandType.Text;
            comm.CommandText = _query;

            con.Open();
            comm.ExecuteNonQuery();
            SqlDataReader reader = comm.ExecuteReader();

            reader.Read();

            return reader.GetInt32(0);
        }

        public string RetrieveUserId(string Username)
        {
            SqlConnection con = new SqlConnection("data source=den1.mssql1.gear.host;initial catalog = timeslice;user id=timeslice;password=Password123!");
            SqlCommand comm = new SqlCommand();
            var _query = "SELECT userId FROM USERS WHERE userName=@username";
            comm.Parameters.AddWithValue("username", Username);
            comm.Connection = con;
            comm.CommandType = CommandType.Text;
            comm.CommandText = _query;

            con.Open();
            comm.ExecuteNonQuery();
            SqlDataReader reader = comm.ExecuteReader();

            reader.Read();

            return reader.GetInt32(0).ToString();
        }

        public bool VerifyPassword(UserLoginModel user)
        {
            SqlConnection con = new SqlConnection("data source=den1.mssql1.gear.host;initial catalog = timeslice;user id=timeslice;password=Password123!");
            SqlCommand comm = new SqlCommand();
            var _query = "SELECT * FROM USERS WHERE userName=@username";
            comm.Parameters.AddWithValue("username", user.Username);
            comm.Connection = con;
            comm.CommandType = CommandType.Text;
            comm.CommandText = _query;

            con.Open();
            comm.ExecuteNonQuery();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                return BCrypt.Net.BCrypt.Verify(user.Password, reader.GetValue(4).ToString());
            }
            return false;
        }

        public bool UserExists(UserSignupModel user)
        {
            SqlConnection con = new SqlConnection("data source=den1.mssql1.gear.host;initial catalog = timeslice;user id=timeslice;password=Password123!");
            SqlCommand comm = new SqlCommand();
            var _query = "SELECT * FROM USERS WHERE userName=@username";
            comm.Parameters.AddWithValue("username", user.Username);
            comm.Connection = con;
            comm.CommandType = CommandType.Text;
            comm.CommandText = _query;

            con.Open();
            comm.ExecuteNonQuery();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                return true;
            }
            return false;
        }

        public void CreateUser(UserSignupModel user)
        {
            SqlConnection con = new SqlConnection("data source=den1.mssql1.gear.host;initial catalog = timeslice;user id=timeslice;password=Password123!");
            SqlCommand comm = new SqlCommand();
            var _query = "INSERT INTO USERS(firstName, lastName, username, password, roleId) VALUES (@firstName, @lastName, @username, @password, 3)";
            comm.Parameters.AddWithValue("username", user.Username);
            comm.Parameters.AddWithValue("password", BCrypt.Net.BCrypt.HashPassword(user.Password));
            comm.Parameters.AddWithValue("firstName", user.FirstName);
            comm.Parameters.AddWithValue("lastName", user.LastName);
            comm.Connection = con;
            comm.CommandType = CommandType.Text;
            comm.CommandText = _query;

            con.Open();
            comm.ExecuteNonQuery();
        }
    }
}
