using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;        // Thư viện cho [Key], [Required]
using System.ComponentModel.DataAnnotations.Schema; // Thư viện cho [Table]

namespace HumanResourceManagementSystem2_0.Model.Class
{
    [Table("Roles")] // Định danh tên bảng
    public class Roles
    {
        // Constructor để khởi tạo danh sách
        public Roles()
        {
            Accounts = new HashSet<Accounts>();
        }

        [Key] // Khóa chính
        public int RoleID { get; set; }

        [Required] // Bắt buộc nhập
        [StringLength(50)] // Giới hạn độ dài (Admin, Employee, Manager...)
        public string RoleName { get; set; }

        // Quan hệ: Một quyền (Role) có thể cấp cho nhiều tài khoản (Accounts)
        public virtual ICollection<Accounts> Accounts { get; set; }
    }
}