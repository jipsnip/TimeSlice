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
        public IActionResult MyCourses()
        {
            IEnumerable<Course> courses = SQL.SelectAllCoursesForUser(HttpContext.Session.GetString("userId"));
            List<CourseInstructorModel> viewCourses = new List<CourseInstructorModel>();
            String courseList = "";
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
            foreach (CourseInstructorModel cim in viewCourses)
            {
                courseList += cim.courseId;
                courseList += " ";
                courseList += cim.courseName;
                courseList += " ";
                courseList += cim.instructorFirstName;
                courseList += ", ";
                courseList += cim.instructorLastName;
                courseList += "\n";
            }
            return Content(courseList);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return Content("Add");
        }

        [HttpPost]
        public IActionResult Add(CourseCreation course)
        {
            return Content("Add");
        }

        [HttpGet]
        public IActionResult Id(int id)
        {
            return Content(id.ToString());
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
    }
}