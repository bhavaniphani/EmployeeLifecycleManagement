using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLifecycleManagement.Models
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeId { get; set; }
        public string Profilepic { get; set; }
        public string JobTitle { get; set; }
        public string EmploymentStatus { get; set; }
        public string UpdatedJobTitle { get; set; }

        public string UpdatedEmploymentStatus { get; set; }

    }
}
