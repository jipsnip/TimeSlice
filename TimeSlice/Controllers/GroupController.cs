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

        [HttpPost]
        [Route("/Project/{projectId}/Group/Add/{groupName}")]
        public IActionResult Add(int projectId, string groupName)
        {
            int groupId = SQL.InsertNewGroup(groupName);
            SQL.addGroupToProject(projectId, groupId);
            return Content(groupId.ToString());
        }

        [HttpGet]
        [Route("/Group/{groupId}")]
        public IActionResult Group(int groupId)
        {
            IEnumerable<User> users = SQL.SelectAllUsersForGroup(groupId.ToString());
            return View("~/Views/Group/GroupUsers.cshtml", users);
        }

        [HttpPost]
        [Route("/Group/User/Add/{groupId}/{username}")]
        public IActionResult AddUser(int groupId, string username)
        {
            if (!SQL.UserExists(username))
            {
                return StatusCode(400);
            }
            SQL = new SQL();
            int userId = SQL.SelectUserIdByUsername(username);
            SQL = new SQL();
            SQL.registerUserForGroup(groupId, userId);
            return StatusCode(200);
        }
    }
}
