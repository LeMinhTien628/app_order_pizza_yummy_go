using System.Text.Json.Serialization;

namespace api_app_pizza_flutter.Models
{
    // Category: Chức năng quản lý danh mục sản phẩm (như Pizza, Đồ uống).
    public class Category
    {
        [JsonPropertyName("CategoryID")]
        public int CategoryId { get; set; } // ID danh mục, khóa chính, tham chiếu trong Products.

        [JsonPropertyName("CategoryName")]
        public string CategoryName { get; set; } = string.Empty; // Tên danh mục, hiển thị trên giao diện.

        [JsonPropertyName("CreatedDate")]
        public DateTime CreatedDate { get; set; } // Ngày tạo danh mục, theo dõi thời gian tạo.
    }
}