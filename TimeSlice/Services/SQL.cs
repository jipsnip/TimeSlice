using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TimeSlice.Services
{
    public class SQL
    {
        /*Global variables for the SQL class 
         *SqlConnection con manages the database connection
         *query is a global string for any SQL query we need to execute
         * SqlCommand comm manages the execution of SQL to the database
        */
        SqlConnection con;
        string query;
        SqlCommand comm;

        /*Constructor for SQL Class*/
        public SQL()
        {
            con = new SqlConnection("data source=den1.mssql1.gear.host;initial catalog = timeslice;user id=timeslice;password=Password123!");
            comm = new SqlCommand();
            comm.Connection = con;
            comm.CommandType = CommandType.Text;
            
        }

        /*Execute takes any sql query string built by any of the following 
         * methods and executes it.  This method sets the command to the query,
         * opens the connection, executes the query, and closes the connection.
         */
        private void Execute(string q)
        {
            query = q;
            comm.CommandText = query;

            con.Open();
            comm.ExecuteNonQuery();
            con.Close();
        }

        /*SelectAllCoursesforUser takes the userID and returns
         *All courses in which the user is enrolled in the case of a student
         *Or returns all courses the user owns in the case of an admin*/
        public void SelectAllCoursesForUser(string user)
        {
            string userId = user;
            query = "SELECT DISTINCT C.courseId, C.courseName FROM USERS U LEFT JOIN GU ON U.userId = GU.userId LEFT JOIN GROUPS G ON GU.groupId = G.groupId LEFT JOIN PG ON G.groupId = PG.groupId LEFT JOIN PROJECTS P ON PG.projectId = P.projectId LEFT JOIN CProj CP ON CP.projectId = P.projectId LEFT JOIN COURSES C ON CP.courseId = C.courseId LEFT JOIN USERS Profs ON C.userId = Profs.userId WHERE (U.userId = '" + userId + "' OR C.userId = '" + userId + "') AND C.courseId IS NOT NULL";
            Execute(query);
        }
        
        public void SelectAllProjectsForCourse()
        {
            
        }

        public void SelectAllGroupsForProject()
        {

        }

        public void SelectAllUsersForGroup()
        {

        }

        public void SelectAllTimesForProject()
        {

        }

        public void CheckUserLogin(string Username, string hashedPassword)
        {
            query = "SELECT * FROM USERS WHERE password=@password AND username=@username";
            comm.Parameters.AddWithValue("password", hashedPassword);
            comm.Parameters.AddWithValue("username", user.Username);
        }
    }
}
