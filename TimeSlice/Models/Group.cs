using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSlice.Models
{
    public class Group
    {
        public String groupName { get; set; }
        public int groupId { get; set; }

        public Group(String _groupName, int _groupId)
        {
            groupName = _groupName;
            groupId = _groupId;
        }
    }
}
