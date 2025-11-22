using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourceManagementSystem2_0.Model.Class
{
    public class TimeSheets
    {
        public int TimeSheetID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime WorkDate { get; set; }
        public TimeSpan? TimeIn { get; set; }
        public TimeSpan? TimeOut { get; set; }
        public double? ActualHours { get; set; }
        public virtual Employees Employee { get; set; }
    }
}
