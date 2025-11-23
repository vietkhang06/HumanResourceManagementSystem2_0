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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HumanResourceManagementSystem2_0
{
    public partial class ForgotPasswordWindow : Window
    {
        // --- Constants for Animation Parameters ---
        // Note: These should match your final, balanced settings from LoginWindow
        private const double InitialScale = 1.2;          // 120% Zoom
        private const double FinalScale = 1.25;          // Gentle zoom to 125%
        private const int MoveRange = 25;                // Max movement in pixels (+/- 25)
        private const int DurationSeconds = 7;           // Duration of Pan/Zoom animation
        private const double FadeDurationSeconds = 1.5;  // Duration of fade in/out
        private const double BorderRadius = 20;

        // Animation Variables
        private readonly DispatcherTimer _timer;
        private readonly List<string> _imagePaths;
        private readonly Random _rnd = new Random();
        private Image? _currentImage;
        private int _lastImageIndex = -1;
        private const string AssemblyName = "HumanResourceManagementSystem2_0";

        public ForgotPasswordWindow()
        {
            InitializeComponent();

            _imagePaths = new List<string>()
            {
                $"pack://application:,,,/{AssemblyName};component/Images/png1.png",
                $"pack://application:,,,/{AssemblyName};component/Images/png2.png",
            };

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            _timer.Tick += (s, e) => ShowNextImage();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateImageClip();
            this.SizeChanged += (s, args) => UpdateImageClip();
            ShowNextImage();
            _timer.Start();
        }

        private void UpdateImageClip()
        {
            if (ImageContainer == null) return;

            double w = ImageContainer.ActualWidth;
            double h = ImageContainer.ActualHeight;

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

        private void ShowNextImage()
        {
            if (_imagePaths.Count == 0) return;

            // 1. Logic to pick the next image
            int nextIndex;
            if (_imagePaths.Count > 1)
            {
                do { nextIndex = _rnd.Next(_imagePaths.Count); } while (nextIndex == _lastImageIndex);
            }
            else nextIndex = 0;

            _lastImageIndex = nextIndex;
            string nextImagePath = _imagePaths[nextIndex];

            // 2. Create the new Image
            Image newImage = new Image
            {
                Stretch = Stretch.UniformToFill,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Opacity = 0,
                RenderTransformOrigin = new Point(0.5, 0.5)
            };

            try
            {
                newImage.Source = new BitmapImage(new Uri(nextImagePath, UriKind.Absolute));
            }
            catch { return; }

            // 3. Apply Scale and Translate Transforms
            TransformGroup group = new TransformGroup();
            TranslateTransform translate = new TranslateTransform();
            ScaleTransform scale = new ScaleTransform(InitialScale, InitialScale);

            group.Children.Add(scale);
            group.Children.Add(translate);
            newImage.RenderTransform = group;

            ImageContainer.Children.Add(newImage);

            // 4. Animations
            TimeSpan moveDuration = TimeSpan.FromSeconds(DurationSeconds);
            TimeSpan fadeDuration = TimeSpan.FromSeconds(FadeDurationSeconds);

            // Fade In
            DoubleAnimation fadeIn = new DoubleAnimation(0, 1, fadeDuration);
            newImage.BeginAnimation(Image.OpacityProperty, fadeIn);

            // Pan Movement
            double toX = _rnd.Next(-MoveRange, MoveRange + 1);
            double toY = _rnd.Next(-MoveRange, MoveRange + 1);

            translate.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation(0, toX, moveDuration));
            translate.BeginAnimation(TranslateTransform.YProperty, new DoubleAnimation(0, toY, moveDuration));

            // Gentle Zoom
            scale.BeginAnimation(ScaleTransform.ScaleXProperty, new DoubleAnimation(InitialScale, FinalScale, moveDuration));
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, new DoubleAnimation(InitialScale, FinalScale, moveDuration));

            // 5. Cleanup Old Image
            if (_currentImage != null)
            {
                Image oldImage = _currentImage;
                DoubleAnimation fadeOut = new DoubleAnimation(1, 0, fadeDuration);
                fadeOut.Completed += (s, e) => ImageContainer.Children.Remove(oldImage);
                oldImage.BeginAnimation(Image.OpacityProperty, fadeOut);
            }

            _currentImage = newImage;
        }

        // --- BUSINESS LOGIC ---

        private void SendResetLink_Click(object sender, RoutedEventArgs e)
        {
            string input = txtUserNameEmail.Text.Trim();

            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Please enter your Username or Email.", "Input Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // --- REAL LOGIC WOULD GO HERE ---

            MessageBox.Show($"If an account exists for {input}, a reset link has been sent.", "Check your email", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        // --- WINDOW EVENTS ---

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
