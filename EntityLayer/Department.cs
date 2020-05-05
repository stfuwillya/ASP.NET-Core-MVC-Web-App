using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer
{
    public class Department
    {
        public int Id { get; set; }
        public string DepName { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
