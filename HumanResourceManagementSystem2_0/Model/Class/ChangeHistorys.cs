using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;        // Cần dòng này để dùng [Key]
using System.ComponentModel.DataAnnotations.Schema; // Cần dòng này để dùng [Table], [ForeignKey]

namespace HumanResourceManagementSystem2_0.Model.Class
{
    [Table("ChangeHistory")] // Đặt tên bảng trong CSDL
    public class ChangeHistorys
    {
        [Key] // <--- BẮT BUỘC: Xác định đây là Khóa Chính
        public int LogID { get; set; }

        [StringLength(100)] // Giới hạn độ dài chuỗi để tối ưu CSDL
        public string TableName { get; set; }

        public int RecordID { get; set; }

        [StringLength(20)]
        public string ActionType { get; set; } // INSERT, UPDATE, DELETE

        public string OldValue { get; set; } // Có thể để max length
        public string NewValue { get; set; }

        public int? ChangeByUserID { get; set; } // FK tới Accounts

        public DateTime ChangeDate { get; set; }

        // --- KHÓA NGOẠI (MỐI QUAN HỆ) ---
        [ForeignKey("ChangeByUserID")] // Chỉ định rõ nó nối với cột ChangeByUserID
        public virtual Accounts Account { get; set; }
    }
}