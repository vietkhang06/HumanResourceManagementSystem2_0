using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourceManagementSystem2_0.Model.Class
{
    public class ChangeHistorys
    {
        public int LogID { get; set; }
        public string TableName { get; set; }
        public int RecordID { get; set; }
        public string ActionType { get; set; } // INSERT, UPDATE, DELETE
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public int? ChangeByUserID { get; set; } // FK tới Accounts
        public DateTime ChangeDate { get; set; }
        public virtual Accounts Account { get; set; }
    }
}
