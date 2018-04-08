using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSlice.Models
{
    public class Course
    {
        public String courseName { get; set; }
        public int userId { get; set; }

        public Course(String _courseName, int _userId)
        {
            courseName = _courseName;
            userId = _userId;
        }
    }
}
