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
    public class NotificationController : Controller
    {
        SQL SQL;

        public NotificationController()
        {
            SQL = new SQL();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/Notifications")]
        public IActionResult Notifications()
        {
            IEnumerable<NotificationUser> notifications = SQL.SelectNotificationsForUser(Convert.ToInt32(HttpContext.Session.GetString("userId")));

            if (HttpContext.Session.GetString("role") == "1")
            {
                return View("~/Views/Notifications/AdminNotifications.cshtml", notifications);
            }
            else
            {
                return View("~/Views/Notifications/Notifications.cshtml", notifications);
            }
        }

        [HttpPost]
        [Route("/Notification/Reject/{notificationId}")]
        public IActionResult Reject(int notificationId)
        {
            SQL.UpdateNotification(notificationId);
            return StatusCode(200);
        }

        [HttpPost]
        [Route("/Notification/Accept/{notificationId}/{username}/{courseName}")]
        public IActionResult Accept(int notificationId, string username, string courseName)
        {
            int userId = SQL.SelectUserIdByUsername(username);
            SQL = new SQL();
            int courseId = SQL.SelectCourseIdByCourseName(courseName);
            SQL = new SQL();
            SQL.registerUserForCourse(userId, courseId);
            SQL = new SQL();
            SQL.UpdateNotification(notificationId);
            return StatusCode(200);
        }
    }
}
