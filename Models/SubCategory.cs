using System.Text.Json.Serialization;

namespace api_app_pizza_flutter.Models
{
    // SubCategory: Chức năng quản lý danh mục con của sản phẩm (như Pizza Hải sản).
    public class SubCategory
    {
        [JsonPropertyName("SubCategoryID")]
        public int SubCategoryId { get; set; } // ID danh mục con, khóa chính, tham chiếu trong Products.

        [JsonPropertyName("CategoryID")]
        public int CategoryId { get; set; } // ID danh mục cha, liên kết với Categories.

        [JsonPropertyName("SubCategoryName")]
        public string SubCategoryName { get; set; } = string.Empty; // Tên danh mục con, hiển thị trên giao diện.

        [JsonPropertyName("CreatedDate")]
        public DateTime CreatedDate { get; set; } // Ngày tạo danh mục con, theo dõi thời gian tạo.
    }
}