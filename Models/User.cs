using System.Text.Json.Serialization;

namespace api_app_pizza_flutter.Models
{
    // User: Chức năng đăng nhập, đăng ký và quản lý thông tin người dùng.
    public class User
    {
        [JsonPropertyName("UserID")]
        public int UserId { get; set; } // ID người dùng, khóa chính, tham chiếu trong Orders, Addresses.

        [JsonPropertyName("UserName")]
        public string UserName { get; set; } = string.Empty; // Tên người dùng, dùng để đăng nhập hoặc hiển thị.

        [JsonPropertyName("Email")]
        public string Email { get; set; } = string.Empty; // Email, dùng để đăng nhập hoặc gửi thông báo.

        [JsonPropertyName("Phone")]
        public string Phone { get; set; } = string.Empty; // Số điện thoại, dùng để liên hệ hoặc xác thực.

        [JsonPropertyName("PasswordHash")]
        public string PasswordHash { get; set; } = string.Empty; // Mật khẩu mã hóa, dùng để xác thực đăng nhập.

        [JsonPropertyName("ProfilePicture")]
        public string? ProfilePicture { get; set; } // URL ảnh đại diện, hiển thị trong hồ sơ, có thể null.

        [JsonPropertyName("CreatedDate")]
        public DateTime CreatedDate { get; set; } // Ngày tạo tài khoản, theo dõi thời gian tạo.
    }
}