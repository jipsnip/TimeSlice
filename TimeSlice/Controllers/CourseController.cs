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

        [HttpGet]
        public IActionResult All()
        {
            if (HttpContext.Session.GetString("role") == "1")
            {
                return Redirect("/Course/MyCourses");
            }
            IEnumerable<Course> courses = SQL.SelectAllCourses();
            List<CourseInstructorModel> viewCourses = new List<CourseInstructorModel>();
            foreach (Course c in courses)
            {
                User instructor = SQL.SelectInstructorByCourse(c.courseId);
                SQL = new SQL();
                CourseInstructorModel cim = new CourseInstructorModel(
                        c.courseName,
                        instructor.Firstname,
                        instructor.Lastname,
                        c.courseId,
                        instructor.Id
                    );
                viewCourses.Add(cim);
            }
            return View("~/Views/Course/AllCourses.cshtml", viewCourses);
        }

        [HttpGet]
        public IActionResult MyCourses()
        {
            IEnumerable<Course> courses = SQL.SelectAllCoursesForUser(HttpContext.Session.GetString("userId"));
            List<CourseInstructorModel> viewCourses = new List<CourseInstructorModel>();

            foreach (Course c in courses)
            {
                User instructor = SQL.SelectInstructorByCourse(c.courseId);
                SQL = new SQL();
                CourseInstructorModel cim = new CourseInstructorModel(
                        c.courseName,
                        instructor.Firstname,
                        instructor.Lastname,
                        c.courseId,
                        instructor.Id
                    );
                viewCourses.Add(cim);
            }
            if (HttpContext.Session.GetString("role") == "1")
            {
                return View("~/Views/Course/AdminCourses.cshtml", viewCourses);
            }
            else
            {
                return View("~/Views/Course/MyCourses.cshtml", viewCourses);
            }
        }

        [HttpPost]
        [Route("/Course/New/{courseName}")]
        public IActionResult New(string courseName)
        {
            int courseId = SQL.InsertNewCourse(courseName, Convert.ToInt32(HttpContext.Session.GetString("userId")));
            return Content(courseId.ToString());
        }

        [HttpPost]
        [Route("/Course/Register/{id}/{courseName}/{instructorId}")]
        public IActionResult Register(int id, string courseName, int instructorId)
        {
            SQL.InsertNotification(HttpContext.Session.GetString("username") + " wants to join the course " + courseName + ".", instructorId);
            return Redirect("/Course/MyCourses");
        }
    }
}