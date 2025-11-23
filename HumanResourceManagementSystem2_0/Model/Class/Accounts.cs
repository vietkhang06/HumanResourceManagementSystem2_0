using System;
using System.ComponentModel.DataAnnotations;        // Cần dòng này để dùng [Key]
using System.ComponentModel.DataAnnotations.Schema; // Cần dòng này để dùng [Table], [ForeignKey]

namespace HumanResourceManagementSystem2_0.Model.Class
{
    [Table("Accounts")]
    public class Accounts
    {
        [Key] // <--- BẮT BUỘC PHẢI CÓ DÒNG NÀY
        public int UserID { get; set; }

        public int EmployeeID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public int RoleID { get; set; }

        public bool IsActive { get; set; }

        // --- NAVIGATION PROPERTIES ---
        [ForeignKey("EmployeeID")]
        public virtual Employees Employee { get; set; }

        [ForeignKey("RoleID")]
        public virtual Roles Role { get; set; }
    }
}