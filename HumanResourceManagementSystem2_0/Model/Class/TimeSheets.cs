using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;        // Thư viện cho [Key]
using System.ComponentModel.DataAnnotations.Schema; // Thư viện cho [Table], [ForeignKey]

namespace HumanResourceManagementSystem2_0.Model.Class
{
    [Table("TimeSheets")] // Định danh tên bảng
    public class TimeSheets
    {
        [Key] // Khóa chính
        public int TimeSheetID { get; set; }

        public int EmployeeID { get; set; } // Khóa ngoại liên kết với Employees

        public DateTime WorkDate { get; set; }

        // TimeSpan là kiểu dữ liệu chuẩn để lưu giờ (ví dụ: 08:30:00)
        public TimeSpan? TimeIn { get; set; }

        public TimeSpan? TimeOut { get; set; }

        public double? ActualHours { get; set; } // Tổng số giờ làm thực tế

        // --- MỐI QUAN HỆ ---
        [ForeignKey("EmployeeID")]
        public virtual Employees Employee { get; set; }
    }
}