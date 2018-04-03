using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeSlice.Middleware;
using TimeSlice.Services;
using Microsoft.AspNetCore.Identity;

namespace TimeSlice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddSessionStateTempDataProvider();

            services.AddDistributedMemoryCache();

            services.AddSession();
            services.AddScoped<IUserData, IUserData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.Run(async context =>
            {
                if (context.Request.Path == "/") // Match with something
                {
                    Console.Write("got here");
                    String course = "test";
                    int user = 10;

                    SqlConnection con = new SqlConnection("data source=den1.mssql1.gear.host;initial catalog = timeslice;user id=timeslice;password=Password123!");
                    string query = "Insert into COURSES (courseName, userId) Values ('" + course + "', '" + user + "')";
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = con;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = query;

                    con.Open();
                    comm.ExecuteNonQuery();

                    String project = "Conways Game of Life";

                    string query1 = "Insert into PROJECTS (projectName) Values ('" + project + "')";
                    SqlCommand comm1 = new SqlCommand();
                    comm1.Connection = con;
                    comm1.CommandType = CommandType.Text;
                    comm1.CommandText = query1;

                    comm1.ExecuteNonQuery();

                    String group = "Team Rocket";

                    string query2 = "Insert into GROUPS (groupName) Values ('" + group + "')";
                    SqlCommand comm2 = new SqlCommand();
                    comm2.Connection = con;
                    comm2.CommandType = CommandType.Text;
                    comm2.CommandText = query2;

                    comm2.ExecuteNonQuery();

                    String firstName = "Eric";
                    String lastName = "Cartman";
                    String userName = "respect my";
                    String password = "authority";
                    int role = 3;

                    string query3 = "Insert into USERS (firstName, lastName, userName, password, roleId) Values ('" + firstName + "', '" + lastName + "', '" + userName + "', '" + password + "', '" + role + "')";
                    SqlCommand comm3 = new SqlCommand();
                    comm3.Connection = con;
                    comm3.CommandType = CommandType.Text;
                    comm3.CommandText = query3;

                    comm3.ExecuteNonQuery();

                    DateTime sTime = DateTime.Now;
                    string sqlDateTime = sTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    string startTime = sqlDateTime;
                    string endTime = sqlDateTime;
                    int pgId = 1;
                    int guId = 1;
                    int cpId = 1;
                    string just = "wow very hard work";


                    string query4 = "Insert into TIMES (startTime, endTime, pgId, justification, guId, cpId) Values ('" + startTime + "', '" + endTime + "', '" + pgId + "', '" + just + "', '" + guId + "', '" + cpId + "')";
                    SqlCommand comm4 = new SqlCommand();
                    comm4.Connection = con;
                    comm4.CommandType = CommandType.Text;
                    comm4.CommandText = query4;

                    comm4.ExecuteNonQuery();

                    
                    string query5 = "UPDATE GU SET groupId = @groupId WHERE userId = @userId";
                    SqlCommand comm5 = new SqlCommand();
                    comm5.Parameters.AddWithValue("groupId", 2);
                    comm5.Parameters.AddWithValue("userId", 1);
                    comm5.Connection = con;
                    comm5.CommandType = CommandType.Text;
                    comm5.CommandText = query5;

                    comm5.ExecuteNonQuery();

                    string query6 = "UPDATE COURSES SET courseName = @courseName WHERE courseId = @courseId";
                    SqlCommand comm6 = new SqlCommand();
                    comm6.Parameters.AddWithValue("courseName", "Basketweaving");
                    comm6.Parameters.AddWithValue("courseId", 1);
                    comm6.Connection = con;
                    comm6.CommandType = CommandType.Text;
                    comm6.CommandText = query6;

                    comm6.ExecuteNonQuery();

                }
                await context.Response.WriteAsync("Hello");
            });

            app.UseSession();

            app.UseMiddleware<MainLoginAuthenticationChecker>();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
