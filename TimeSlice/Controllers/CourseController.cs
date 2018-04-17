using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeSlice.Models;
using TimeSlice.Services;
using TimeSlice.ViewModels;

namespace TimeSlice.Controllers
{
    public class CourseController : Controller
    {
        SQL SQL;

        public CourseController()
        {
            SQL = new SQL();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult All()
        {
            if(HttpContext.Session.GetString("role") == "1")
            {
                return Redirect("/Course/MyCourses");
            }
            IEnumerable<Course> courses = SQL.SelectAllCourses();
            List<CourseInstructorModel> viewCourses = new List<CourseInstructorModel>();
            foreach(Course c in courses){
                User instructor = SQL.SelectInstructorByCourse(c.courseId);
                SQL = new SQL();
                CourseInstructorModel cim = new CourseInstructorModel(
                        c.courseName,
                        instructor.Firstname,
                        instructor.Lastname,
                        c.courseId
                    );
                viewCourses.Add(cim);
            }
            return View("~/Views/Courses/AllCourses.cshtml", viewCourses);
        }

        [HttpGet]
        [Route("/Course/MyCourses")]
        public IActionResult MyCourses()
        {
            IEnumerable<Course> courses = SQL.SelectAllCoursesForUser(HttpContext.Session.GetString("userId"));
            List<CourseInstructorModel> viewCourses = new List<CourseInstructorModel>();

            foreach(Course c in courses)
            {
                User instructor = SQL.SelectInstructorByCourse(c.courseId);
                SQL = new SQL();
                CourseInstructorModel cim = new CourseInstructorModel(
                        c.courseName,
                        instructor.Firstname,
                        instructor.Lastname,
                        c.courseId
                    );
                viewCourses.Add(cim);
            }
            return View("~/Views/Courses/MyCourses.cshtml", viewCourses);
        }

        [HttpPost]
        [Route("/Course/New/{courseName}")]
        public IActionResult New(string courseName)
        {
            SQL.InsertNewCourse(courseName, Convert.ToInt32(HttpContext.Session.GetString("userId")));
            return StatusCode(200);
        }

        [HttpGet]
        [Route("/Course/{courseId}/Projects")]
        public IActionResult Id(int courseId)
        {
            return Content(courseId.ToString());
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            return Content("");
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult UpdateCourse(int id)
        {
            return Content("");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            return Content("");
        }

        [HttpPost]
        public IActionResult Register(int id)
        {
            SQL.registerUserForCourse(Convert.ToInt32(HttpContext.Session.GetString("userId")), id);
            return Redirect("/Course/MyCourses");
        }
    }
}