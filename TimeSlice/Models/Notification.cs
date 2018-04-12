using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSlice.Models
{
    public class Notification
    {
        public String notifMessage { get; set; }
        public int isActive { get; set; }

        public Notification(String _notifMessage, int _isActive)
        {
            notifMessage = _notifMessage;
            isActive = _isActive;
        }
    }
}
