using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using TimeSlice.Models;
using TimeSlice.ViewModels;

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
        SqlDataReader reader;

        /*Constructor for SQL Class*/
        public SQL()
        {
            con = new SqlConnection("data source=den1.mssql1.gear.host;initial catalog = timeslice;user id=timeslice;password=Password123!");
            comm = new SqlCommand();
            comm.Connection = con;
            comm.CommandType = CommandType.Text;
            con.Open();
        }

        /*SelectAllCoursesforUser takes the userID and returns
         *All courses in which the user is enrolled in the case of a student
         *Or returns all courses the user owns in the case of an admin*/
        public IEnumerable<Course> SelectAllCoursesForUser(string user)
        {
            string userId = user;
            // Old query -- query = "SELECT DISTINCT C.courseId, C.courseName FROM USERS U LEFT JOIN GU ON U.userId = GU.userId LEFT JOIN GROUPS G ON GU.groupId = G.groupId LEFT JOIN PG ON G.groupId = PG.groupId LEFT JOIN PROJECTS P ON PG.projectId = P.projectId LEFT JOIN CProj CP ON CP.projectId = P.projectId LEFT JOIN COURSES C ON CP.courseId = C.courseId LEFT JOIN USERS Profs ON C.userId = Profs.userId WHERE (U.userId = @userId OR C.userId = @userId) AND C.courseId IS NOT NULL";
            query = "SELECT DISTINCT C.courseId, C.courseName FROM USERS U INNER JOIN CU ON U.userId = CU.userId INNER JOIN COURSES C ON CU.courseId = C.courseId WHERE U.userId = @userId";
            comm.Parameters.AddWithValue("userId", userId);
            comm.CommandText = query;
            comm.ExecuteNonQuery();

            reader = comm.ExecuteReader();

            List<Course> courseList = new List<Course>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Course course = new Course(
                        reader.GetString(1),
                        reader.GetInt32(0)
                        );
                    courseList.Add(course);
                }
            }
            reader.Close();
            return courseList;

        }

        /*SelectAllProjectsForCourse takes the courseId and returns 
         * all projects for the given courseId */
        public IEnumerable<Project> SelectAllProjectsForCourse(string courseId)
        {
            string CourseID = courseId;
            query = "SELECT DISTINCT P.projectId, P.projectName FROM COURSES C INNER JOIN CProj ON C.courseId = CProj.courseId LEFT JOIN PROJECTS P ON CProj.projectId = P.projectId WHERE C.courseId = @courseId";
            comm.Parameters.AddWithValue("courseId", CourseID);
            comm.CommandText = query;
            comm.ExecuteNonQuery();

            reader = comm.ExecuteReader();

            List<Project> projectList = new List<Project>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Project project = new Project(
                        reader.GetInt32(0),
                        reader.GetString(1)
                        );
                    projectList.Add(project);
                }
            }
            reader.Close();
            return projectList;

        }

        /*SelectAllGroupsForProject takes the projectId and returns all groups
         for the specified project*/
        public IEnumerable<Group> SelectAllGroupsForProject(string project)
        {
            string projectId = project;
            query = "SELECT G.groupName FROM GROUPS G INNER JOIN PG ON G.groupId = PG.groupId LEFT JOIN PROJECTS P ON PG.projectId = PG.projectId WHERE P.projectId = @projectId GROUP BY G.groupName";
            comm.Parameters.AddWithValue("projectId", projectId);
            comm.CommandText = query;
            comm.ExecuteNonQuery();

            reader = comm.ExecuteReader();

            List<Group> groupList = new List<Group>();
            if (reader.HasRows)
            {
                while(reader.Read())
                { 
                    Group group = new Group(
                        reader.GetString(0)
                        );
                    groupList.Add(group);
                }
            }
            reader.Close();
            return groupList;
        }

        /*SelectAllUsersForGroup takes the groupId and returns all users
        registered for the specified group*/
        public IEnumerable<User> SelectAllUsersForGroup(string groupId)
        {
            query = "";
            comm.Parameters.AddWithValue("groupId", groupId);
            comm.CommandText = query;
            comm.ExecuteNonQuery();
            List<User> userList = new List<User>();
            if (reader.HasRows)
            {
                while(reader.Read())
                {
                    User user = new User(
                        reader.GetInt16(1),
                        reader.GetString(0),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetInt16(5)
                        );
                    userList.Add(user);
                }
            }
            reader.Close();
            return userList;

        }

        /*SelectAllTimesForProject takes the projectId and returns all
        time entries logged against the specified project*/
        public IEnumerable<Time> SelectAllTimesForProject(int projectId)
        {
            List<Time> timeSlices = new List<Time>();
            query = "SELECT T.timeSliceId, T.startTime, T.endTime, T.pgId, T.justification, T.guId, T.cpId FROM TIMES T INNER JOIN CProj CP ON T.cpId = CP.cpId WHERE CP.projectId = @projectId";
            comm.Parameters.AddWithValue("projectId", projectId);
            comm.CommandText = query;
            comm.ExecuteNonQuery();

            reader = comm.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Time time = new Time(
                        reader.GetDateTime(0),
                        reader.GetDateTime(1),
                        reader.GetInt32(2),
                        reader.GetString(3),
                        reader.GetInt32(4),
                        reader.GetInt32(5));
                    timeSlices.Add(time);
                }
            }
            reader.Close();
            return timeSlices;
        }

        /*SelectAllTimesForUser takes the userId and returns all times
        logged for the specified user*/
        public IEnumerable<Time> SelectAllTimesForUser(int userId)
        {
            List<Time> timeSlices = new List<Time>();
            query = "select * from TIMES inner join GU on TIMES.guId = GU.guId where GU.userId = @userId;";
            comm.Parameters.AddWithValue("userId", userId);
            comm.CommandText = query;
            comm.ExecuteNonQuery();

            reader = comm.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Time time = new Time(
                        reader.GetDateTime(0),
                        reader.GetDateTime(1),
                        reader.GetInt32(2),
                        reader.GetString(3),
                        reader.GetInt32(4),
                        reader.GetInt32(5));
                    timeSlices.Add(time);
                }
            }
            reader.Close();
            return timeSlices;
        }

        public IEnumerable<Course> SelectAllCourses()
        {
            List<Course> courses = new List<Course>();
            query = "SELECT * FROM COURSES";
            comm.CommandText = query;
            comm.ExecuteNonQuery();

            reader = comm.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Course course = new Course(
                            reader.GetString(1),
                            reader.GetInt32(0)
                        );
                    courses.Add(course);
                }
            }
            reader.Close();
            return courses;
        }

        public User SelectInstructorByCourse(int courseId)
        {
            query = "SELECT * FROM USERS INNER JOIN CU ON USERS.userId = CU.userId WHERE USERS.roleId = 1 AND CU.courseId = @cId";
            comm.Parameters.AddWithValue("cId", courseId);
            comm.CommandText = query;
            comm.ExecuteNonQuery();

            reader = comm.ExecuteReader();

            reader.Read();
            User myUser = new User(
                reader.GetInt32(0),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetInt32(5)
           );
            reader.Close();
            return myUser;
            
        }

        public bool UserBelongsToCourse(string userId, int courseId)
        {
            query = "SELECT * FROM CU WHERE userId = @userId AND courseId = @courseId";
            comm.Parameters.AddWithValue("userId", userId);
            comm.Parameters.AddWithValue("courseId", courseId);
            comm.CommandText = query;
            comm.ExecuteNonQuery();

            reader = comm.ExecuteReader();

            if (reader.HasRows)
            {
                return true;
            }
            return false;
        }

        public bool UserExists(UserSignupModel user)
        {
            var _query = "SELECT * FROM USERS WHERE userName=@username";
            comm.Parameters.AddWithValue("username", user.Username);
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

        public bool VerifyPassword(UserLoginModel user)
        {
            var _query = "SELECT * FROM USERS WHERE userName=@username";
            comm.Parameters.AddWithValue("username", user.Username);
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

        public int SelectProjectIdByName(string projectName)
        {
            query = "SELECT projectId FROM PROJECTS WHERE projectName = @projectName";
            comm.Parameters.AddWithValue("projectName", projectName);
            comm.CommandText = query;
            comm.ExecuteNonQuery();

            reader = comm.ExecuteReader();

            reader.Read();
            int pId = reader.GetInt32(0);
            reader.Close();
            return pId;
        }

        //inserts
        public void InsertNewCourse(String courseName, int userId)
        {
            string query = "Insert into COURSES (courseName, userId) Values (@courseName, @userId)";
            comm.Parameters.AddWithValue("courseName", courseName);
            comm.Parameters.AddWithValue("userId", userId);
            comm.CommandText = query;
            comm.ExecuteNonQuery();
        }

        public void InsertNewProject(String projectName)
        {
            string query = "Insert into PROJECTS (projectName) Values (@projectName)";
            comm.Parameters.AddWithValue("projectName", projectName);
            comm.CommandText = query;
            comm.ExecuteNonQuery();
        }

        public void InsertNewGroup(String groupName)
        {
            string query = "Insert into GROUPS (groupName) Values (@groupName)";
            comm.Parameters.AddWithValue("groupName", groupName);
            comm.CommandText = query;
            comm.ExecuteNonQuery();
        }

        public void CreateUser(UserSignupModel user)
        {
            var _query = "INSERT INTO USERS(firstName, lastName, username, password, roleId) VALUES (@firstName, @lastName, @username, @password, 2)";
            comm.Parameters.AddWithValue("username", user.Username);
            comm.Parameters.AddWithValue("password", BCrypt.Net.BCrypt.HashPassword(user.Password));
            comm.Parameters.AddWithValue("firstName", user.FirstName);
            comm.Parameters.AddWithValue("lastName", user.LastName);
            comm.CommandText = _query;
            con.Open();
            comm.ExecuteNonQuery();
        }

        public void InsertTimeSlice(DateTime startTime, DateTime endTime, int pgId, string just, int guId, int cpId)
        {
            string sqlStartTime = startTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string sqlEndTime = endTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string query = "Insert into TIMES (startTime, endTime, pgId, justification, guId, cpId) Values (@startTime, @endTime, @pgId, @just, @guId, @cpId)";
            comm.Parameters.AddWithValue("startTime", sqlStartTime);
            comm.Parameters.AddWithValue("endTime", sqlEndTime);
            comm.Parameters.AddWithValue("pgId", pgId);
            comm.Parameters.AddWithValue("just", just);
            comm.Parameters.AddWithValue("guId", guId);
            comm.Parameters.AddWithValue("cpId", cpId);
            comm.CommandText = query;
            comm.ExecuteNonQuery();
        }

        public void InsertNotification(string notification)
        {
            string query = "Insert into NOTIFICATIONS (notificationMessage, isActive) Values (@message, 1)";
            comm.Parameters.AddWithValue("message", notification);
            comm.CommandText = query;
            comm.ExecuteNonQuery();
        }

        public void addProjectToCourse(int courseId, int projectId)
        {
            string query = "Insert into CU (courseId, projectId) Values (@course, @project)";
            comm.Parameters.AddWithValue("course", courseId);
            comm.Parameters.AddWithValue("project", projectId);
            comm.CommandText = query;
            comm.ExecuteNonQuery();
        }

        public void addGroupToProject(int projectId, int groupId)
        {
            string query = "Insert into CU (projectId, groupId) Values (@project, @group)";
            comm.Parameters.AddWithValue("project", projectId);
            comm.Parameters.AddWithValue("group", groupId);
            comm.CommandText = query;
            comm.ExecuteNonQuery();
        }

        public void registerUserForCourse(int userId, int courseId)
        {
            string query = "Insert into CU (userId, courseId) Values (@user, @course)";
            comm.Parameters.AddWithValue("user", userId);
            comm.Parameters.AddWithValue("course", courseId);
            comm.CommandText = query;
            comm.ExecuteNonQuery();
        }

        public void registerUserForGroup(int groupId, int userId)
        {
            string query = "Insert into GU (groupId, userId) Values (@group, @user)";
            comm.Parameters.AddWithValue("group", groupId);
            comm.Parameters.AddWithValue("user", userId);
            comm.CommandText = query;
            comm.ExecuteNonQuery();
        }

        //updates
        public void UpdateCourseName(String courseName, int courseId)
        {
            query = "UPDATE COURSES SET courseName = @courseName WHERE courseId = @courseId";
            comm.Parameters.AddWithValue("courseName", courseName);
            comm.Parameters.AddWithValue("courseId", courseId);
            comm.CommandText = query;
            comm.ExecuteNonQuery();
        }

        public void UpdateGroupName(String groupName, int groupId)
        {
            query = "UPDATE COURSES SET groupName = @groupName WHERE groupId = @groupId";
            comm.Parameters.AddWithValue("groupName", groupName);
            comm.Parameters.AddWithValue("groupId", groupId);
            comm.CommandText = query;
            comm.ExecuteNonQuery();
        }

        public void UpdateYourGroup(string groupId, int userId)
        {
            string query = "UPDATE GU SET groupId = @groupId WHERE userId = @userId";
            comm.Parameters.AddWithValue("groupId", groupId);
            comm.Parameters.AddWithValue("userId", userId);
            comm.CommandText = query;
            comm.ExecuteNonQuery();
        }

        public void UpdateYourPassword(string password, int userId)
        {
            string query = "UPDATE USERS SET password = @password WHERE userId = @userId";
            comm.Parameters.AddWithValue("password", password);
            comm.Parameters.AddWithValue("userId", userId);
            comm.CommandText = query;
            comm.ExecuteNonQuery();
        }

        public void UpdateNotification(int notifId)
        {
            string query = "UPDATE NOTIFICATIONS SET isActive = 0 WHERE notificationId = @notificationId";
            comm.Parameters.AddWithValue("notificationId", notifId);
            comm.CommandText = query;
            comm.ExecuteNonQuery();
        }

        /*When a user switches groups less than halfway through the course,
         * the time previously logged for that user to the project should
         * be reallocated */
        public void ReassignTimeSlices(User oldUser, Project project, Group group, Course course)
        {

        }

    }
}
