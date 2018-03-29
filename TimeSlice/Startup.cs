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

                    SqlConnection con1 = new SqlConnection("data source=den1.mssql1.gear.host;initial catalog = timeslice;user id=timeslice;password=Password123!");
                    string query1 = "Insert into PROJECTS (projectName) Values (" + project + ")";
                    SqlCommand comm1 = new SqlCommand();
                    comm1.Connection = con;
                    comm1.CommandType = CommandType.Text;
                    comm1.CommandText = query1;

                    con1.Open();
                    comm1.ExecuteNonQuery();
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
