using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSlice.Models
{
    public class Course
    {
        public String courseName { get; set; }
        public int courseId { get; set; }

        public Course(String _courseName, int _courseId)
        {
            courseName = _courseName;
            courseId = _courseId;
        }
    }
}
