using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewGeneric.Models
{
    public class EmployeeViewModel
    {
        public int EId { get; set; }
        public string EName { get; set; }
        public int ESalary { get; set; }
        public int EDepartmentId { get; set; }
        public string EDepartmentName { get; set; }
    }
}
