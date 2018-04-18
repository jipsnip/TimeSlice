using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSlice.ViewModels
{
    public class CourseInstructorModel
    {
        public String courseName { get; set; }
        public String instructorFirstName { get; set; }
        public String instructorLastName { get; set; }
        public int instructorId { get; set; }
        public int courseId { get; set; }

        public CourseInstructorModel(String _courseName, String _instructorFirstName, String _instructorLastName, int _courseId, int _instructorId)
        {
            courseName = _courseName;
            instructorFirstName = _instructorFirstName;
            instructorLastName = _instructorLastName;
            courseId = _courseId;
            instructorId = _instructorId;
        }
    }
}
