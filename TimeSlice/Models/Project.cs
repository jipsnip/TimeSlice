using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSlice.Models
{
    public class Project
    {
        public String projectName { get; set; }

        public Project(String _projectName)
        {
            projectName = _projectName;
        }
    }
}
