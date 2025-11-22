using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourceManagementSystem2_0.Model.Class
{
    public class Accounts
    {
        public int UserID { get; set; }
        public int EmployeeID { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public int RoleID { get; set; }
        public bool IsActive { get; set; }
        public virtual Employees Employee { get; set; }
        public virtual Roles Role { get; set; }
    }
}
