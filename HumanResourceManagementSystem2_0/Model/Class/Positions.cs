using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourceManagementSystem2_0.Model.Class
{
    public class Positions
    {
        public Positions()
        {
            Employees = new HashSet<Employees>();
        }
        public int PositionID { get; set; }
        public string PositionName { get; set; }
        public string JobDescription { get; set; }
        public int? SalaryGrade { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
