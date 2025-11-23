using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;       // Thư viện cho [Key], [StringLength]
using System.ComponentModel.DataAnnotations.Schema; // Thư viện cho [Table], [ForeignKey]

namespace HumanResourceManagementSystem2_0.Model.Class
{
    [Table("LeaveRequests")]
    public class LeaveRequests
    {
        [Key]
        public int RequestID { get; set; }

        public int EmployeeID { get; set; } // FK của người xin nghỉ

        [StringLength(50)]
        public string LeaveType { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalDays { get; set; }

        [StringLength(20)]
        public string Status { get; set; } // Pending, Approved, Rejected

        public int? ApproverID { get; set; } // FK của người duyệt

        [StringLength(50)]
        public string ApprovalStep { get; set; }

        public string ManagerComment { get; set; }

        // --- MỐI QUAN HỆ (NAVIGATION PROPERTIES) ---

        // 1. Người xin nghỉ (Nối với EmployeeID)
        [ForeignKey("EmployeeID")]
        public virtual Employees Requester { get; set; }

        // 2. Người duyệt (Nối với ApproverID)
        [ForeignKey("ApproverID")]
        public virtual Employees Approver { get; set; }
    }
}