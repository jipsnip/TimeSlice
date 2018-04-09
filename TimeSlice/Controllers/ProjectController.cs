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
            List<Project> projects = (List<Project>)SQL.SelectAllProjectsForCourse(courseId.ToString());
            String projectList = "";
            foreach(Project p in projects)
            {
                projectList += p.projectId;
                projectList += " ";
                projectList += p.projectName;
                projectList += "\n";
            }
            return Content(projectList);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return Content("");
        }

        [HttpPost]
        public IActionResult Add(ProjectCreation project)
        {
            return Content("");
        }

        [HttpGet]
        public IActionResult Update(int projectId)
        {
            return Content("");
        }

        [HttpPost]
        public IActionResult Update(ProjectCreation project)
        {
            return Content("");
        }
    }
}
