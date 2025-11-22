using System.Windows;

namespace HumanResourceManagementSystem2_0
{
    public partial class MainDashboard : Window
    {
        // Biến lưu quyền hiện tại
        private string _userRole;

        // Sửa hàm khởi tạo để nhận tham số role
        public MainDashboard(string role)
        {
            InitializeComponent();
            _userRole = role;

            // Gọi hàm phân quyền ngay khi mở form
            PhanQuyenGiaoDien();
        }

        private void PhanQuyenGiaoDien()
        {
            if (_userRole == "Admin")
            {
                // 1. Load màn hình Admin vào giữa
                MainContent.Content = new Dashboard_Admin();
                txtPageTitle.Text = "Trang Chủ Admin";

                // 2. Hiển thị đầy đủ nút
                btnManageEmp.Visibility = Visibility.Visible;
                btnDepartments.Visibility = Visibility.Visible;
                btnPayrollAdmin.Visibility = Visibility.Visible;

                // 3. Ẩn nút của nhân viên (nếu muốn)
                btnProfile.Visibility = Visibility.Collapsed;
                btnTimeSheet.Visibility = Visibility.Collapsed;
            }
            else if (_userRole == "Employee")
            {
                // 1. Load màn hình Nhân viên vào giữa
                MainContent.Content = new Dashboard_Employee();
                txtPageTitle.Text = "Trang Chủ Nhân Viên";

                // 2. Ẩn các nút quản lý của Admin
                btnManageEmp.Visibility = Visibility.Collapsed;
                btnDepartments.Visibility = Visibility.Collapsed;
                btnPayrollAdmin.Visibility = Visibility.Collapsed;

                // 3. Hiện nút của nhân viên
                btnProfile.Visibility = Visibility.Visible;
                btnTimeSheet.Visibility = Visibility.Visible;
            }
        }

        // ... (Giữ nguyên các hàm xử lý nút bấm khác của bạn) ...

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            // Khi bấm Home thì quay lại Dashboard tương ứng
            if (_userRole == "Admin") MainContent.Content = new Dashboard_Admin();
            else MainContent.Content = new Dashboard_Employee();
        }
        private void btnManageEmp_Click(object sender, RoutedEventArgs e)
        {
            // Mở trang quản lý
            // MainContent.Content = new EmployeeManagementView();
            txtPageTitle.Text = "Quản Lý Nhân Viên";
        }
    }
}