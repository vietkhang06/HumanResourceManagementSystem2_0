using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace HumanResourceManagementSystem2_0
{
    public partial class Dashboard_Admin : UserControl
    {
        public Dashboard_Admin()
        {
            InitializeComponent();
            LoadDummyData();
        }

        private void LoadDummyData()
        {
            // Tạo dữ liệu giả giống trong hình
            var list = new List<RequestItem>
            {
                new RequestItem { EmployeeID="#WT01", Name="Tanmay Vatsa", Position="UI/UX Designer", Department="Technology", LeaveType="Casual Leave", LeaveColorBg="#E3F2FD", LeaveColorFg="#2196F3", Duration="7 days", DateApplied="15 Apr 2023" },
                new RequestItem { EmployeeID="#WT34", Name="Anirudha Bagchi", Position="Sr. Backend Developer", Department="Technology", LeaveType="Sick Leave", LeaveColorBg="#FFF3E0", LeaveColorFg="#FF9800", Duration="12 days", DateApplied="13 Apr 2023" },
                new RequestItem { EmployeeID="#WT43", Name="Ranjana Mehra", Position="Sr Manager (Sales)", Department="Sales", LeaveType="Maternity Leave", LeaveColorBg="#F3E5F5", LeaveColorFg="#9C27B0", Duration="172 days", DateApplied="12 Sep 2023" },
                new RequestItem { EmployeeID="#WT23", Name="Ravindra Sharma", Position="Sr. Marketing Manager", Department="Marketing", LeaveType="Paternity Leave", LeaveColorBg="#FFF8E1", LeaveColorFg="#FFC107", Duration="8 days", DateApplied="17 Apr 2023" },
            };

            dgRequests.ItemsSource = list;
        }
    }

    // Class phụ để chứa dữ liệu hiển thị
    public class RequestItem
    {
        public string EmployeeID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string LeaveType { get; set; }
        public string LeaveColorBg { get; set; } // Màu nền Badge (Hex code)
        public string LeaveColorFg { get; set; } // Màu chữ Badge
        public string Duration { get; set; }
        public string DateApplied { get; set; }
        public string AvatarPath { get; set; } = "/Images/png1.png"; // Đường dẫn ảnh mặc định
    }
}