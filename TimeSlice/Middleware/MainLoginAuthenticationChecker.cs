using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSlice.Middleware
{
    public class MainLoginAuthenticationChecker
    {

        private readonly RequestDelegate _next;

        public MainLoginAuthenticationChecker(RequestDelegate next){
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Session.GetString("LoggedIn") != "true")
            {
                if (context.Request.Path != "/Account" && 
                    context.Request.Path != "/Account/Login" && 
                    context.Request.Path != "/Account/Signup")
                {
                    context.Response.Redirect("/Account/Login");
                }
                else
                {
                    await _next(context);
                }
            }
            else
            {
                if (context.Session.GetString("role") == "3")
                {
                    await context.Response.WriteAsync("You do not yet have permissions to access the contents of the website. You must wait for approval.");
                }
                else
                {
                    await Task.Run(() =>
                    {
                        _next(context);
                    });
                }
            }
        }
    }
}
