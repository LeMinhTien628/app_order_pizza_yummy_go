using System.Text.Json.Serialization;

namespace api_app_pizza_flutter.Models
{
    // Address: Chức năng quản lý địa chỉ giao hàng của người dùng.
    public class Address
    {
        [JsonPropertyName("AddressID")]
        public int AddressId { get; set; } // ID địa chỉ, khóa chính, tham chiếu trong Orders.

        [JsonPropertyName("UserID")]
        public int UserId { get; set; } // ID người dùng, liên kết với Users.

        [JsonPropertyName("AddressName")]
        public string AddressName { get; set; } = string.Empty; // Tên địa chỉ, hiển thị khi chọn giao hàng.

        [JsonPropertyName("IsDefault")]
        public bool IsDefault { get; set; } // Địa chỉ mặc định, ưu tiên khi đặt hàng.

        [JsonPropertyName("CreatedDate")]
        public DateTime CreatedDate { get; set; } // Ngày tạo địa chỉ, theo dõi thời gian tạo.
    }
}