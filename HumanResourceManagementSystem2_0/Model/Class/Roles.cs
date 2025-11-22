using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourceManagementSystem2_0.Model.Class
{
    public class Roles
    {
        public int RoleID { get; set; }

        public string RoleName { get; set; }

        public virtual ICollection<Accounts> Accounts { get; set; }
    }
}
