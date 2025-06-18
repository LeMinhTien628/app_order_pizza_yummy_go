using System.Text.Json.Serialization;

namespace api_app_pizza_flutter.Models
{
    // Order: Chức năng quản lý đơn hàng của người dùng.
    public class Order
    {
        [JsonPropertyName("OrderID")]
        public int OrderId { get; set; } // ID đơn hàng, khóa chính, tham chiếu trong OrderDetails.

        [JsonPropertyName("UserID")]
        public int UserId { get; set; } // ID người dùng, liên kết với Users.

        [JsonPropertyName("AddressID")]
        public int? AddressId { get; set; } // ID địa chỉ giao hàng, liên kết với Addresses, có thể null.

        [JsonPropertyName("OrderDate")]
        public DateTime OrderDate { get; set; } // Ngày đặt hàng, theo dõi thời gian đặt.

        [JsonPropertyName("TotalAmount")]
        public decimal TotalAmount { get; set; } // Tổng giá trị đơn hàng, hiển thị trên giao diện.

        [JsonPropertyName("Status")]
        public string Status { get; set; } = string.Empty; // Trạng thái đơn hàng (ví dụ: Đang xử lý, Đã giao).

        [JsonPropertyName("PaymentMethod")]
        public string PaymentMethod { get; set; } = string.Empty; // Phương thức thanh toán (ví dụ: Tiền mặt, Thẻ).

        [JsonPropertyName("ShippingFee")]
        public decimal ShippingFee { get; set; } // Phí vận chuyển, dùng để tính tổng giá trị.

        [JsonPropertyName("PromotionID")]
        public int? PromotionId { get; set; } // ID khuyến mãi, liên kết với Promotions, có thể null.

        [JsonPropertyName("CreatedDate")]
        public DateTime CreatedDate { get; set; } // Ngày tạo đơn hàng, theo dõi thời gian tạo.
       
    }
}