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
        public IActionResult Notifications()
        {
            IEnumerable<NotificationUser> notifications = SQL.SelectNotificationsForUser(23);
            List<NotificationUser> notifs = new List<NotificationUser>();
            foreach (NotificationUser n in notifs)
            {
                NotificationUser num = new NotificationUser(
                        n.message,
                        n.isActive,
                        n.userId
                    );
                notifs.Add(num);
            }
            if (HttpContext.Session.GetString("role") == "1")
            {
                return View("~/Views/Notifications/AdminNotifications.cshtml", notifs);
            }
            else
            {
                return View("~/Views/Notifications/Notifications.cshtml", notifs);
            }
        }
    }
}
