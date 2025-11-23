using Microsoft.EntityFrameworkCore;
using HumanResourceManagementSystem2_0.Model.Class;

namespace HumanResourceManagementSystem2_0.Data
{
    public class HrmsDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=HrmsDatabase.db");
        }

        // --- ĐOẠN QUAN TRỌNG NHẤT ĐỂ SỬA LỖI ---
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Cấu hình cho mối quan hệ "Người Xin Nghỉ" (Requester)
            modelBuilder.Entity<LeaveRequests>()
                .HasOne(l => l.Requester)        // Một đơn có 1 người xin
                .WithMany(e => e.LeaveRequests)  // Một người có nhiều đơn xin (Trỏ vào list LeaveRequests bên Employee)
                .HasForeignKey(l => l.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict);

            // 2. Cấu hình cho mối quan hệ "Người Duyệt" (Approver)
            modelBuilder.Entity<LeaveRequests>()
                .HasOne(l => l.Approver)         // Một đơn có 1 người duyệt
                .WithMany()                      // Bên Employee KHÔNG CẦN list chứa các đơn đã duyệt (để trống)
                .HasForeignKey(l => l.ApproverID)
                .OnDelete(DeleteBehavior.Restrict);
        }
        // ---------------------------------------

        // Khai báo các bảng (Giữ nguyên tên số nhiều như file bạn đã tạo)
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Positions> Positions { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<TimeSheets> TimeSheets { get; set; }
        public DbSet<LeaveRequests> LeaveRequests { get; set; }
        public DbSet<WorkContracts> WorkContracts { get; set; }
        public DbSet<ChangeHistorys> ChangeHistories { get; set; }
    }
}