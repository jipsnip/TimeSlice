using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSlice.Models;
using TimeSlice.Services;

namespace TimeSlice.Controllers
{
    public class GroupController : Controller
    {

        SQL SQL;

        public GroupController()
        {
            SQL = new SQL();
        }

        [HttpGet]
        [Route("/Course/{courseId}/Project/{projectName}")]
        public IActionResult All(int courseId, string projectName)
        {
            int projectId = SQL.SelectProjectIdByName(projectName);
            List<Group> groups = new List<Group>();

            groups = (List<Group>)SQL.SelectAllGroupsForProject(projectId.ToString());

            return View("~/Views/Group/ProjectGroups.cshtml", groups);
        }

        [HttpGet]
        [Route("/Course/Project/Group/New")]
        public IActionResult New()
        {
            return Content("New");
        }
    }
}
