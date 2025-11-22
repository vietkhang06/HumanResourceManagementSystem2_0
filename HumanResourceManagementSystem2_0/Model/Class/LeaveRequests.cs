using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourceManagementSystem2_0.Model.Class
{
    public class LeaveRequests
    {
        public int RequestID { get; set; }
        public int EmployeeID { get; set; }
        public string LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalDays { get; set; }
        public string Status { get; set; } // Pending, Approved, Rejected
        public int? ApproverID { get; set; } // Người duyệt (FK tới Employee)
        public string ApprovalStep { get; set; }
        public string ManagerComment { get; set; }
        public virtual Employees Requester { get; set; }
        public virtual Employees Approver { get; set; }
    }
}
