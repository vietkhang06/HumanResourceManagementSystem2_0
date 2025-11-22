using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourceManagementSystem2_0.Model.Class
{
    public class Employees
    {
        public Employees()
        {
            Accounts = new HashSet<Accounts>();
            WorkContracts = new HashSet<WorkContracts>();
            TimeSheets = new HashSet<TimeSheets>();
            LeaveRequests = new HashSet<LeaveRequests>();
            Subordinates = new HashSet<Employees>(); // Nhân viên cấp dưới
        }
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }

        // Khóa ngoại
        public int? DepartmentID { get; set; }
        public int? PositionID { get; set; }
        public int? ManagerID { get; set; } // Người quản lý trực tiếp
        public DateTime? HireDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal? Salary { get; set; }
        public string Status { get; set; } // Active, On Leave, Terminated
        public virtual Departments Department { get; set; }
        public virtual Positions Position { get; set; }
        public virtual Employees Manager { get; set; } // Self-referencing
        public virtual ICollection<Employees> Subordinates { get; set; } // Danh sách cấp dưới
        public virtual ICollection<Accounts> Accounts { get; set; }
        public virtual ICollection<WorkContracts> WorkContracts { get; set; }
        public virtual ICollection<TimeSheets> TimeSheets { get; set; }
        public virtual ICollection<LeaveRequests> LeaveRequests { get; set; }
    }
}
