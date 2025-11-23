using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;        // Thư viện cho [Key], [Required]
using System.ComponentModel.DataAnnotations.Schema; // Thư viện cho [Table]

namespace HumanResourceManagementSystem2_0.Model.Class
{
    [Table("Positions")] // Định danh tên bảng trong CSDL
    public class Positions
    {
        public Positions()
        {
            // Khởi tạo danh sách nhân viên thuộc chức vụ này
            Employees = new HashSet<Employees>();
        }

        [Key] // Khóa chính
        public int PositionID { get; set; }

        [Required] // Bắt buộc phải có tên chức vụ
        [StringLength(100)] // Giới hạn độ dài tên (ví dụ: "Trưởng phòng kỹ thuật")
        public string PositionName { get; set; }

        public string JobDescription { get; set; } // Mô tả công việc (có thể để dài hoặc null)

        public int? SalaryGrade { get; set; } // Bậc lương

        // Quan hệ: Một chức vụ có nhiều nhân viên
        public virtual ICollection<Employees> Employees { get; set; }
    }
}