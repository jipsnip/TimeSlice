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
    public class ProjectController : Controller
    {
        SQL SQL;

        public ProjectController()
        {
            SQL = new SQL();
        }

        [HttpGet]
        [Route("/Course/{courseId}/Projects")]
        public IActionResult All(int courseId)
        {
            if (!SQL.UserBelongsToCourse(HttpContext.Session.GetString("userId"), courseId))
            {
                return Redirect("/Course/MyCourses");
            }
            SQL = new SQL();
            List<Project> projects = (List<Project>)SQL.SelectAllProjectsForCourse(courseId.ToString());
            return View("~/Views/Project/CourseProjects.cshtml", projects);
        }

        [HttpPost]
        [Route("/Project/Add/{projectName}/{courseId}")]
        public IActionResult Add(int courseId, string projectName)
        {
            if(!SQL.UserBelongsToCourse(HttpContext.Session.GetString("userId"), courseId))
            {
                return StatusCode(401);
            }
            int projectId = SQL.InsertNewProject(projectName, courseId);
            return Content(projectId.ToString());
        }

        [HttpGet]
        [Route("/Course/Project/{projectId}")]
        public IActionResult Project(int projectId)
        {
            IEnumerable<Group> groups = SQL.SelectAllGroupsForProject(projectId.ToString());
            return View("~/Views/Group/ProjectGroups.cshtml", groups);
        }

        [HttpGet]
        public IActionResult Update(int projectId)
        {
            return Content("");
        }

        [HttpPost]
        public IActionResult Update()
        {
            return Content("");
        }
    }
}
