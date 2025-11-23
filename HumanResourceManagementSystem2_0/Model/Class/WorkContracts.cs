using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;        // Thư viện cho [Key], [StringLength]
using System.ComponentModel.DataAnnotations.Schema; // Thư viện cho [Table], [ForeignKey]

namespace HumanResourceManagementSystem2_0.Model.Class
{
    [Table("WorkContracts")] // Định danh tên bảng
    public class WorkContracts
    {
        [Key] // Khóa chính
        public int ContractID { get; set; }

        public int EmployeeID { get; set; } // Khóa ngoại

        [StringLength(50)] // Loại hợp đồng (Full-time, Part-time, CTV...)
        public string ContractType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; } // Có thể null nếu là hợp đồng vô thời hạn

        // --- MỐI QUAN HỆ ---
        [ForeignKey("EmployeeID")]
        public virtual Employees Employee { get; set; }
    }
}