using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSlice.ViewModels
{
    public class NotificationUser
    {
        public String message { get; set; }
        public int isActive { get; set; }
        public int userId { get; set; }

        public NotificationUser(String _message, int _isActive, int _userId)
        {
            message = _message;
            isActive = _isActive;
            userId = _userId;
        }
    }
}
