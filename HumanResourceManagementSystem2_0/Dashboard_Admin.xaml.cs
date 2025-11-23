using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HumanResourceManagementSystem2_0
{
    public partial class Dashboard_Admin : UserControl
    {
        private List<RequestItem> _pendingList;
        private List<RequestItem> _approvedList;
        private List<RequestItem> _rejectedList;

        private Brush _colorActive = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2ECC71")); // Xanh lá
        private Brush _colorInactive = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#95A5A6")); // Xám

        public Dashboard_Admin()
        {
            InitializeComponent();
            LoadDummyData();
            SwitchTab("Pending"); // Mặc định vào tab Pending
        }

        private void LoadDummyData()
        {
            // 1. Pending Data
            _pendingList = new List<RequestItem>
            {
                new RequestItem { EmployeeID="#WT01", Name="Tanmay Vatsa", Position="UI/UX Designer", Department="Technology", LeaveType="Casual Leave", LeaveColorBg="#E3F2FD", LeaveColorFg="#2196F3", Duration="7 days", DateApplied="15 Apr 2023" },
                new RequestItem { EmployeeID="#WT34", Name="Anirudha Bagchi", Position="Sr. Backend Developer", Department="Technology", LeaveType="Sick Leave", LeaveColorBg="#FFF3E0", LeaveColorFg="#FF9800", Duration="12 days", DateApplied="13 Apr 2023" },
                new RequestItem { EmployeeID="#WT43", Name="Ranjana Mehra", Position="Sr Manager (Sales)", Department="Sales", LeaveType="Maternity Leave", LeaveColorBg="#F3E5F5", LeaveColorFg="#9C27B0", Duration="172 days", DateApplied="12 Sep 2023" }
            };

            // 2. Approved Data
            _approvedList = new List<RequestItem>
            {
                new RequestItem { EmployeeID="#WT88", Name="John Doe", Position="HR Manager", Department="Human Resource", LeaveType="Annual Leave", LeaveColorBg="#E8F5E9", LeaveColorFg="#2E7D32", Duration="5 days", DateApplied="10 Apr 2023" },
                new RequestItem { EmployeeID="#WT99", Name="Sarah Smith", Position="Accountant", Department="Finance", LeaveType="Sick Leave", LeaveColorBg="#FFF3E0", LeaveColorFg="#FF9800", Duration="1 day", DateApplied="01 Apr 2023" }
            };

            // 3. Rejected Data
            _rejectedList = new List<RequestItem>
            {
                new RequestItem { EmployeeID="#WT12", Name="Mike Tyson", Position="Security", Department="Admin", LeaveType="Casual Leave", LeaveColorBg="#FFEBEE", LeaveColorFg="#C62828", Duration="2 days", DateApplied="20 Mar 2023" }
            };
        }

        // --- SỰ KIỆN CLICK TAB ---
        private void TabPending_Click(object sender, System.Windows.Input.MouseButtonEventArgs e) => SwitchTab("Pending");
        private void TabApproved_Click(object sender, System.Windows.Input.MouseButtonEventArgs e) => SwitchTab("Approved");
        private void TabRejected_Click(object sender, System.Windows.Input.MouseButtonEventArgs e) => SwitchTab("Rejected");

        // --- LOGIC CHUYỂN TAB ---
        private void SwitchTab(string tabName)
        {
            // Reset giao diện
            txtTabPending.Foreground = _colorInactive; bdrTabPending.Visibility = Visibility.Collapsed;
            txtTabApproved.Foreground = _colorInactive; bdrTabApproved.Visibility = Visibility.Collapsed;
            txtTabRejected.Foreground = _colorInactive; bdrTabRejected.Visibility = Visibility.Collapsed;

            // Cập nhật theo Tab được chọn
            if (tabName == "Pending")
            {
                txtTabPending.Foreground = _colorActive;
                bdrTabPending.Visibility = Visibility.Visible;
                dgRequests.ItemsSource = _pendingList;
                pnlActionButtons.Visibility = Visibility.Visible; // Hiện nút duyệt
            }
            else if (tabName == "Approved")
            {
                txtTabApproved.Foreground = _colorActive;
                bdrTabApproved.Visibility = Visibility.Visible;
                dgRequests.ItemsSource = _approvedList;
                pnlActionButtons.Visibility = Visibility.Collapsed; // Ẩn nút
            }
            else // Rejected
            {
                txtTabRejected.Foreground = _colorActive;
                bdrTabRejected.Visibility = Visibility.Visible;
                dgRequests.ItemsSource = _rejectedList;
                pnlActionButtons.Visibility = Visibility.Collapsed; // Ẩn nút
            }
        }
    }

    // Class Model phụ trợ
    public class RequestItem
    {
        public string EmployeeID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string LeaveType { get; set; }
        public string LeaveColorBg { get; set; }
        public string LeaveColorFg { get; set; }
        public string Duration { get; set; }
        public string DateApplied { get; set; }
        public string AvatarPath { get; set; } = "/Images/png1.png";
    }
}