using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;        // Thư viện cho [Key], [Required]
using System.ComponentModel.DataAnnotations.Schema; // Thư viện cho [Table], [ForeignKey]

namespace HumanResourceManagementSystem2_0.Model.Class
{
    [Table("Employees")]
    public class Employees
    {
        public Employees()
        {
            // Khởi tạo các danh sách để tránh lỗi NullReferenceException
            Accounts = new HashSet<Accounts>();
            WorkContracts = new HashSet<WorkContracts>();
            TimeSheets = new HashSet<TimeSheets>();
            LeaveRequests = new HashSet<LeaveRequests>();
            Subordinates = new HashSet<Employees>();
        }

        [Key] // Khóa chính
        public int EmployeeID { get; set; }

        [Required] // Bắt buộc nhập
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        // --- CÁC KHÓA NGOẠI (FOREIGN KEYS) ---
        public int? DepartmentID { get; set; }
        public int? PositionID { get; set; }
        public int? ManagerID { get; set; } // ID của người quản lý

        public DateTime? HireDate { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public decimal? Salary { get; set; }

        [StringLength(20)]
        public string Status { get; set; } // Active, On Leave, Terminated

        // --- MỐI QUAN HỆ (NAVIGATION PROPERTIES) ---

        [ForeignKey("DepartmentID")]
        public virtual Departments Department { get; set; }

        [ForeignKey("PositionID")]
        public virtual Positions Position { get; set; }

        [ForeignKey("ManagerID")]
        public virtual Employees Manager { get; set; } // Tự tham chiếu (Sếp cũng là nhân viên)

        public virtual ICollection<Employees> Subordinates { get; set; } // Danh sách cấp dưới

        public virtual ICollection<Accounts> Accounts { get; set; }
        public virtual ICollection<WorkContracts> WorkContracts { get; set; }
        public virtual ICollection<TimeSheets> TimeSheets { get; set; }

        // Danh sách đơn xin nghỉ (Đã được cấu hình kỹ trong DbContext)
        public virtual ICollection<LeaveRequests> LeaveRequests { get; set; }
    }
}