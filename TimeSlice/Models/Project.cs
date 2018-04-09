using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSlice.Models
{
    public class Project
    {
        public String projectName { get; set; }
        public int projectId { get; set; }

        public Project(int _projectId, String _projectName)
        {
            projectName = _projectName;
            projectId = _projectId;
        }
    }
}
