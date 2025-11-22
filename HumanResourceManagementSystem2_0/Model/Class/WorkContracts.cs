using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourceManagementSystem2_0.Model.Class
{
    public class WorkContracts
    {
        public int ContractID { get; set; }
        public int EmployeeID { get; set; }
        public string ContractType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual Employees Employee { get; set; }
    }
}
