using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation; // Thêm namespace cho Animation
using System.Windows.Media.Imaging;
using System.Windows.Threading;       // Thêm namespace cho Timer
using HumanResourceManagementSystem2_0.Data;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagementSystem2_0
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        // --- CÁC HẰNG SỐ VÀ BIẾN CHO ANIMATION (ƯU ĐIỂM TỪ CODE THAM KHẢO) ---
        private const double InitialScale = 1.2;         // Zoom ban đầu 120%
        private const double FinalScale = 1.25;          // Zoom nhẹ lên 125%
        private const int MoveRange = 25;                // Phạm vi di chuyển pixel (+/- 25)
        private const int DurationSeconds = 7;           // Thời lượng hiệu ứng Pan/Zoom
        private const double FadeDurationSeconds = 1.5;  // Thời lượng mờ dần
        private const double BorderRadius = 20;          // Độ bo góc khớp với XAML
        private const string AssemblyName = "HumanResourceManagementSystem2_0"; // Tên Project để load ảnh

        private readonly List<string> _imagePaths;
        private readonly DispatcherTimer _timer;
        private readonly Random _rnd = new Random();
        private Image? _currentImage;
        private int _lastImageIndex = -1;

        public LoginWindow()
        {
            InitializeComponent();

            // Cấu hình danh sách ảnh (Dùng đường dẫn tuyệt đối pack URI để ổn định hơn)
            _imagePaths = new List<string>()
            {
                $"pack://application:,,,/{AssemblyName};component/Images/png1.png",
                $"pack://application:,,,/{AssemblyName};component/Images/png2.png"
            };

            // Khởi tạo Timer cho Slideshow
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5) // Chuyển ảnh mỗi 5 giây
            };
            _timer.Tick += (s, e) => ShowNextImage();
        }

        // Sự kiện khi Window tải xong
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Thiết lập vùng cắt bo góc và bắt đầu Slideshow
            UpdateImageClip();
            this.SizeChanged += (s, args) => UpdateImageClip(); // Cập nhật lại nếu cửa sổ đổi kích thước

            ShowNextImage(); // Hiện ảnh đầu tiên ngay lập tức
            _timer.Start();  // Bắt đầu đếm giờ
        }

        // --- CÁC HÀM XỬ LÝ GIAO DIỆN & ANIMATION ---

        // Hàm tạo vùng cắt (Clip) bo tròn chính xác cho Container
        private void UpdateImageClip()
        {
            if (ImageContainer == null) return;

            double w = ImageContainer.ActualWidth;
            double h = ImageContainer.ActualHeight;

            // Vẽ hình chữ nhật bo góc bằng Code để làm mặt nạ cắt (Mask)
            StreamGeometry geom = new StreamGeometry();
            using (StreamGeometryContext ctx = geom.Open())
            {
                ctx.BeginFigure(new Point(0, 0), true, true);
                ctx.LineTo(new Point(w - BorderRadius, 0), true, false);
                ctx.ArcTo(new Point(w, BorderRadius), new Size(BorderRadius, BorderRadius), 0, false, SweepDirection.Clockwise, true, false);
                ctx.LineTo(new Point(w, h - BorderRadius), true, false);
                ctx.ArcTo(new Point(w - BorderRadius, h), new Size(BorderRadius, BorderRadius), 0, false, SweepDirection.Clockwise, true, false);
                ctx.LineTo(new Point(0, h), true, false);
            }

            ImageContainer.Clip = geom;
        }

        // Hàm hiển thị ảnh tiếp theo với hiệu ứng Pan & Zoom + Fade
        private void ShowNextImage()
        {
            if (_imagePaths.Count == 0) return;

            // Chọn ảnh ngẫu nhiên nhưng không trùng ảnh vừa hiển thị
            int nextIndex;
            if (_imagePaths.Count > 1)
            {
                do { nextIndex = _rnd.Next(_imagePaths.Count); } while (nextIndex == _lastImageIndex);
            }
            else
            {
                nextIndex = 0;
            }

            _lastImageIndex = nextIndex;
            string nextImagePath = _imagePaths[nextIndex];

            // Tạo đối tượng Image mới
            Image newImage = new Image
            {
                Stretch = Stretch.UniformToFill,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Opacity = 0, // Bắt đầu ẩn để Fade In
                RenderTransformOrigin = new Point(0.5, 0.5)
            };

            try
            {
                newImage.Source = new BitmapImage(new Uri(nextImagePath, UriKind.Absolute));
            }
            catch
            {
                // Nếu lỗi load ảnh thì bỏ qua lần này
                return;
            }

            // Chuẩn bị Transform Group cho hiệu ứng Zoom (Scale) và Di chuyển (Translate)
            TransformGroup group = new TransformGroup();
            TranslateTransform translate = new TranslateTransform();
            ScaleTransform scale = new ScaleTransform(InitialScale, InitialScale);

            group.Children.Add(scale);
            group.Children.Add(translate);
            newImage.RenderTransform = group;

            // Thêm vào giao diện
            ImageContainer.Children.Add(newImage);

            // --- Bắt đầu Animation ---
            TimeSpan moveDuration = TimeSpan.FromSeconds(DurationSeconds);
            TimeSpan fadeDuration = TimeSpan.FromSeconds(FadeDurationSeconds);

            // 1. Fade In (Hiện dần)
            DoubleAnimation fadeIn = new DoubleAnimation(0, 1, fadeDuration);
            newImage.BeginAnimation(Image.OpacityProperty, fadeIn);

            // 2. Pan Movement (Di chuyển nhẹ ngẫu nhiên)
            double toX = _rnd.Next(-MoveRange, MoveRange + 1);
            double toY = _rnd.Next(-MoveRange, MoveRange + 1);

            translate.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation(0, toX, moveDuration));
            translate.BeginAnimation(TranslateTransform.YProperty, new DoubleAnimation(0, toY, moveDuration));

            // 3. Gentle Zoom (Phóng to nhẹ)
            scale.BeginAnimation(ScaleTransform.ScaleXProperty, new DoubleAnimation(InitialScale, FinalScale, moveDuration));
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, new DoubleAnimation(InitialScale, FinalScale, moveDuration));

            // 4. Cleanup Old Image (Xóa ảnh cũ sau khi ảnh mới hiện xong)
            if (_currentImage != null)
            {
                Image oldImage = _currentImage;
                DoubleAnimation fadeOut = new DoubleAnimation(1, 0, fadeDuration);
                fadeOut.Completed += (s, e) => ImageContainer.Children.Remove(oldImage);
                oldImage.BeginAnimation(Image.OpacityProperty, fadeOut);
            }

            _currentImage = newImage;
        }

        // --- EVENT HANDLERS CƠ BẢN ---

        // Kéo thả cửa sổ
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        // Nút Login (Giữ nguyên logic Database của bạn)
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUserID.Text;
            string password = txtPassword.Password;

            using (var context = new HrmsDbContext())
            {
                // Tìm tài khoản trong CSDL
                var user = context.Accounts
                                  .Include(a => a.Role) // Kèm theo thông tin Quyền
                                  .FirstOrDefault(u => u.UserName == username && u.PasswordHash == password);

                if (user != null)
                {
                    if (user.IsActive == false)
                    {
                        MessageBox.Show("Tài khoản này đã bị khóa!");
                        return;
                    }

                    // Đăng nhập thành công -> Mở MainDashboard theo Quyền
                    string roleName = user.Role.RoleName; // "Admin" hoặc "Employee"

                    MainDashboard main = new MainDashboard(roleName);
                    main.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                }
            }
        }

        // Nút Tắt
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}