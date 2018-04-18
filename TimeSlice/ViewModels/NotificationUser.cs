using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSlice.ViewModels
{
    public class NotificationUser
    {
        public String message { get; set; }
        public Boolean isActive { get; set; }
        public int notificationId { get; set; }
        public int userId { get; set; }

        public NotificationUser(String _message, Boolean _isActive, int _userId, int _notificationId)
        {
            message = _message;
            isActive = _isActive;
            userId = _userId;
            notificationId = _notificationId;
        }
    }
}
