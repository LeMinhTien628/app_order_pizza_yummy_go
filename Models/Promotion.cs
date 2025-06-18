using System.Text.Json.Serialization;

namespace api_app_pizza_flutter.Models
{
    // Promotion: Chức năng quản lý chương trình khuyến mãi.
    public class Promotion
    {
        [JsonPropertyName("PromotionID")]
        public int PromotionId { get; set; } // ID khuyến mãi, khóa chính, tham chiếu trong Orders.

        [JsonPropertyName("PromotionName")]
        public string PromotionName { get; set; } = string.Empty; // Tên khuyến mãi, hiển thị trên giao diện.

        [JsonPropertyName("Description")]
        public string? Description { get; set; } // Mô tả khuyến mãi, hiển thị chi tiết, có thể null.

        [JsonPropertyName("DiscountPercentage")]
        public decimal? DiscountPercentage { get; set; } // Phần trăm giảm giá, dùng để tính giá trị đơn hàng, có thể null.

        [JsonPropertyName("ImageURL")]
        public string? ImageUrl { get; set; } // URL ảnh khuyến mãi, hiển thị trên giao diện, có thể null.

        [JsonPropertyName("StartDate")]
        public DateTime StartDate { get; set; } // Ngày bắt đầu khuyến mãi, kiểm tra thời gian áp dụng.

        [JsonPropertyName("EndDate")]
        public DateTime EndDate { get; set; } // Ngày kết thúc khuyến mãi, kiểm tra thời gian áp dụng.

        [JsonPropertyName("CreatedDate")]
        public DateTime CreatedDate { get; set; } // Ngày tạo khuyến mãi, theo dõi thời gian tạo.
    }
}