using System.Windows;
using System.Linq;
using HumanResourceManagementSystem2_0.Data;
using HumanResourceManagementSystem2_0.Model.Class;

namespace HumanResourceManagementSystem2_0
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Gọi hàm tạo dữ liệu mẫu trước khi mở màn hình Login
            SeedDatabase();

            // Mở màn hình Login
            LoginWindow login = new LoginWindow();
            login.Show();
        }

        private void SeedDatabase()
        {
            using (var context = new HrmsDbContext())
            {
                // Tạo CSDL nếu chưa có (đảm bảo file .db luôn tồn tại)
                context.Database.EnsureCreated();

                // Kiểm tra xem đã có quyền Admin chưa, nếu chưa thì tạo
                if (!context.Roles.Any())
                {
                    // 1. Tạo Quyền (Roles)
                    var roleAdmin = new Roles { RoleName = "Admin" };
                    var roleEmp = new Roles { RoleName = "Employee" };
                    context.Roles.AddRange(roleAdmin, roleEmp);
                    context.SaveChanges();

                    // 2. Tạo Nhân viên Admin (Employees)
                    var empAdmin = new Employees
                    {
                        FirstName = "Quản Trị",
                        LastName = "Viên",
                        Email = "admin@hrms.com",
                        Status = "Active"
                    };
                    context.Employees.Add(empAdmin);
                    context.SaveChanges();

                    // 3. Tạo Tài khoản Admin (Accounts)
                    var accAdmin = new Accounts
                    {
                        EmployeeID = empAdmin.EmployeeID,
                        UserName = "admin",
                        PasswordHash = "123", // Lưu tạm 123 (Thực tế nên mã hóa)
                        RoleID = roleAdmin.RoleID,
                        IsActive = true
                    };
                    context.Accounts.Add(accAdmin);
                    context.SaveChanges();
                }
            }
        }
    }
}