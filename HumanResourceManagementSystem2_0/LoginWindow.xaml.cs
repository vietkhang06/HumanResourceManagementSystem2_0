using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HumanResourceManagementSystem2_0
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        // Sự kiện khi Window tải xong
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetRandomBackgroundImage();
        }

        // Hàm Random ảnh nền (Đã sửa để tương thích với Border bo góc)
        private void SetRandomBackgroundImage()
        {
            try
            {
                List<string> imagePaths = new List<string>()
                {
                    "images/png1.png",
                    "images/png2.png",
                    // Thêm ảnh khác vào đây nếu muốn
                };

                Random rnd = new Random();
                int index = rnd.Next(imagePaths.Count);

                // 1. Tạo BitmapImage từ đường dẫn
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                // Lưu ý: Đảm bảo Build Action của ảnh là "Resource"
                bitmap.UriSource = new Uri($"pack://application:,,,/{imagePaths[index]}", UriKind.Absolute);
                bitmap.EndInit();

                // 2. Tạo ImageBrush (Bút vẽ bằng ảnh)
                ImageBrush imgBrush = new ImageBrush();
                imgBrush.ImageSource = bitmap;
                imgBrush.Stretch = Stretch.UniformToFill;
                imgBrush.Opacity = 0.9;

                // 3. Gán Brush này làm nền cho Border bên phải
                RightBorder.Background = imgBrush;
            }
            catch (Exception ex)
            {
                // Nếu lỗi (ví dụ không tìm thấy ảnh), set màu nền mặc định để không bị trắng bệch
                RightBorder.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                MessageBox.Show("Không thể tải ảnh nền: " + ex.Message);
            }
        }

        // Kéo thả cửa sổ
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        // Nút Login
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUserID.Text;     // Lấy tên đăng nhập
            string password = txtPassword.Password; // Lấy mật khẩu

            // CASE 1: Đăng nhập ADMIN
            if (username == "admin" && password == "123")
            {
                // Mở MainDashboard với quyền "Admin"
                MainDashboard main = new MainDashboard("Admin");
                main.Show();
                this.Close();
            }
            // CASE 2: Đăng nhập EMPLOYEE
            else if (username == "employee" && password == "123")
            {
                // Mở MainDashboard với quyền "Employee"
                MainDashboard main = new MainDashboard("Employee");
                main.Show();
                this.Close();
            }
            // CASE 3: Sai thông tin
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Lỗi Đăng Nhập", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Nút Tắt
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
