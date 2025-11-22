using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourceManagementSystem2_0.Model.Class
{
    public class Departments
    {
        public Departments()
        {
            Employees = new HashSet<Employees>();
        }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int? ManagerID { get; set; } // Trưởng phòng (FK)
        public string Location { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
