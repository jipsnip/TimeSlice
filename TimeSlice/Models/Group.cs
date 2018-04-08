using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSlice.Models
{
    public class Group
    {
        public String groupName { get; set; }

        public Group(String _groupName)
        {
            groupName = _groupName;
        }
    }
}
