using System.Text.Json.Serialization;

namespace api_app_pizza_flutter.Models
{
    // Suggestion: Chức năng gợi ý sản phẩm liên quan cho người dùng.
    public class Suggestion
    {
        [JsonPropertyName("SuggestionID")]
        public int SuggestionId { get; set; } // ID gợi ý, khóa chính.

        [JsonPropertyName("ProductID")]
        public int ProductId { get; set; } // ID sản phẩm gốc, liên kết với Products.

        [JsonPropertyName("SuggestedProductID")]
        public int SuggestedProductId { get; set; } // ID sản phẩm gợi ý, liên kết với Products.

        [JsonPropertyName("Confidence")]
        public decimal Confidence { get; set; } // Độ tin cậy của gợi ý, từ 0-100.

        [JsonPropertyName("CreatedDate")]
        public DateTime CreatedDate { get; set; } // Ngày tạo gợi ý, theo dõi thời gian.
    }
}