using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSlice.ViewModels
{
    public class CourseInstructorModel
    {
        public String courseName;
        public String instructorFirstName;
        public String instructorLastName;
        public int courseId;
        public CourseInstructorModel(String _courseName, String _instructorFirstName, String _instructorLastName, int _courseId)
        {
            courseName = _courseName;
            instructorFirstName = _instructorFirstName;
            instructorLastName = _instructorLastName;
            courseId = _courseId;
        }
    }
}
