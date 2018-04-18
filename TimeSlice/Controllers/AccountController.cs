using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeSlice.Models;
using TimeSlice.Services;
using TimeSlice.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeSlice.Controllers
{
    public class AccountController : Controller
    {
        private IUserData _userData;

        public AccountController(IUserData userData)
        {
            _userData = userData;
        }

        // GET: /Account/
        [Route("/Account")]
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        // GET: /Account/Login/
        [HttpGet]
        [Route("/Account/Login")]
        public IActionResult Login()
        {
            UserLoginModel model = new UserLoginModel();
            model.valid = true;
            return View(model);
        }

        [HttpPost]
        [Route("/Account/Login")]
        [ValidateAntiForgeryToken]
        // POST: /Account/Login/
        public IActionResult Login(UserLoginModel loginModel)
        {
            //ModelState is a property that is affected by properties that were set in /ViewModels/UserLoginModel
            //These properties can add validation to posts and IsValid tells you if the post passed any required tests
            //In this case both Username and Password are required fields so ModelState will not be valid if they are empty
            if (ModelState.IsValid)
            {
                //Service that will look up the user in the database and check the username/password combination
                //If it is valid they will be redirected and their Session["LoggedIn"] will be set
                //If it isnt valid it will send back a Http 204 error for the front end to deal with
                bool valid = _userData.VerifyPassword(loginModel);
                if (valid)
                {
                    int role = _userData.FindRole(loginModel.Username);
                    HttpContext.Session.SetString("LoggedIn", "true");
                    HttpContext.Session.SetString("username", loginModel.Username);
                    HttpContext.Session.SetString("userId", _userData.RetrieveUserId(loginModel.Username));
                    HttpContext.Session.SetString("role", role.ToString());
                    return Redirect("/");
                }
                else
                {
                    loginModel.valid = false;
                    return View(loginModel);
                }
            }
            else
            {
                //This will reshow the view with error messages like "The Username field is required"
                return View();
            }
        }

        [HttpGet]
        [Route("/Account/Logout")]
        // GET: /Account/Logout/
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Account/Login");
        }

        [HttpGet]
        [Route("/Account/Signup")]
        // GET: /Account/Signup/
        public IActionResult Signup()
        {
            UserSignupModel signupModel = new UserSignupModel();
            signupModel.Available = true;
            return View(signupModel);
        }

        [HttpPost]
        [Route("/Account/Signup")]
        // POST: /Account/Signup/
        public IActionResult Signup(UserSignupModel signupModel)
        {
            if (ModelState.IsValid)
            {
                bool available = !(_userData.UserExists(signupModel));
                if (available)
                {
                    _userData.CreateUser(signupModel);
                    HttpContext.Session.SetString("LoggedIn", "true");
                    HttpContext.Session.SetString("username", signupModel.Username);
                    HttpContext.Session.SetString("userId", _userData.RetrieveUserId(signupModel.Username));
                    HttpContext.Session.SetString("role", "3");
                    return Redirect("/");
                }
                else
                {
                    signupModel.Available = false;
                    return View(signupModel);
                }
            }
            else
            {
                return View();
            }
        }
    }
}
