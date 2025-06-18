using System.Text.Json.Serialization;

namespace api_app_pizza_flutter.Models
{
    // Product: Chức năng quản lý thông tin sản phẩm (như Pizza, Đồ uống).
    public class Product
    {
        [JsonPropertyName("ProductID")]
        public int ProductId { get; set; } // ID sản phẩm, khóa chính, tham chiếu trong OrderDetails.

        [JsonPropertyName("ProductName")]
        public required string ProductName { get; set; } // Tên sản phẩm, hiển thị trên giao diện.

        [JsonPropertyName("Description")]
        public string? Description { get; set; } // Mô tả sản phẩm, hiển thị chi tiết sản phẩm, có thể null.

        [JsonPropertyName("Price")]
        public decimal Price { get; set; } // Giá sản phẩm, dùng để tính toán đơn hàng.

        [JsonPropertyName("CategoryID")]
        public int CategoryId { get; set; } // ID danh mục, liên kết với Categories.

        [JsonPropertyName("SubCategoryID")]
        public int? SubCategoryId { get; set; } // ID danh mục con, liên kết với SubCategories, có thể null.

        [JsonPropertyName("ImageURL")]
        public string? ImageUrl { get; set; } // URL ảnh sản phẩm, hiển thị trên giao diện, có thể null.

        [JsonPropertyName("CreatedDate")]
        public DateTime CreatedDate { get; set; } // Ngày tạo sản phẩm, theo dõi thời gian tạo.

    }
}