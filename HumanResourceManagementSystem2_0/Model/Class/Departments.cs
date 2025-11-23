using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;        // Cần dòng này
using System.ComponentModel.DataAnnotations.Schema; // Cần dòng này

namespace HumanResourceManagementSystem2_0.Model.Class
{
    [Table("Departments")]
    public class Departments
    {
        public Departments()
        {
            Employees = new HashSet<Employees>();
        }

        [Key]
        public int DepartmentID { get; set; }

        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; }

        public int? ManagerID { get; set; } // Trưởng phòng (Lưu ID nhân viên)

        [StringLength(200)]
        public string Location { get; set; }

        // Quan hệ: Một phòng ban có nhiều nhân viên
        public virtual ICollection<Employees> Employees { get; set; }
    }
}