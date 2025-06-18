using System.Text.Json.Serialization;

namespace api_app_pizza_flutter.Models
{
    // Feedback: Chức năng quản lý đánh giá của người dùng về đơn hàng.
    public class Feedback
    {
        [JsonPropertyName("FeedbackID")]
        public int FeedbackId { get; set; } // ID đánh giá, khóa chính.

        [JsonPropertyName("ProductID")]
        public int ProductId { get; set; } // ID sản phẩm, liên kết với Products.
        [JsonPropertyName("OrderID")]
        public int OrderId { get; set; } // ID đơn hàng, liên kết với Orders.

        [JsonPropertyName("UserID")]
        public int UserId { get; set; } // ID người dùng, liên kết với Users.

        [JsonPropertyName("CrustRating")]
        public byte CrustRating { get; set; } // Điểm đánh giá đế, từ 1-5.

        [JsonPropertyName("SauceRating")]
        public byte SauceRating { get; set; } // Điểm đánh giá nước sốt, từ 1-5.

        [JsonPropertyName("CheeseRating")]
        public byte CheeseRating { get; set; } // Điểm đánh giá phô mai, từ 1-5.

        [JsonPropertyName("ToppingRating")]
        public byte ToppingRating { get; set; } // Điểm đánh giá topping, từ 1-5.

        [JsonPropertyName("OverallTasteRating")]
        public byte OverallTasteRating { get; set; } // Điểm đánh giá tổng thể, từ 1-5.

        [JsonPropertyName("PresentationRating")]
        public byte PresentationRating { get; set; } // Điểm đánh giá trình bày, từ 1-5.

        [JsonPropertyName("ServiceRating")]
        public byte ServiceRating { get; set; } // Điểm đánh giá dịch vụ, từ 1-5.

        [JsonPropertyName("Comment")]
        public string? Comment { get; set; } // Bình luận, mô tả chi tiết đánh giá, có thể null.

        [JsonPropertyName("CreatedDate")]
        public DateTime CreatedDate { get; set; } // Ngày tạo đánh giá, theo dõi thời gian.

    }
}