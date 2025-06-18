using System.Text.Json.Serialization;

namespace api_app_pizza_flutter.Models
{
    // PromotionProduct: Chức năng liên kết sản phẩm với chương trình khuyến mãi.
    public class PromotionProduct
    {
        [JsonPropertyName("PromotionProductID")]
        public int PromotionProductId { get; set; } // ID liên kết, khóa chính.

        [JsonPropertyName("PromotionID")]
        public int PromotionId { get; set; } // ID khuyến mãi, liên kết với Promotions.

        [JsonPropertyName("ProductID")]
        public int ProductId { get; set; } // ID sản phẩm, liên kết với Products.
    }
}