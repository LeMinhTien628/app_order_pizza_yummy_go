using System.Text.Json.Serialization;

namespace api_app_pizza_flutter.Models
{
    // Topping: Chức năng quản lý topping cho sản phẩm (như Phô mai, Xúc xích).
    public class Topping
    {
        [JsonPropertyName("ToppingID")]
        public int ToppingId { get; set; } // ID topping, khóa chính, tham chiếu trong OrderDetails.

        [JsonPropertyName("ToppingName")]
        public string ToppingName { get; set; } = string.Empty; // Tên topping, hiển thị trên giao diện.

        [JsonPropertyName("Price")]
        public decimal Price { get; set; } // Giá topping, dùng để tính toán đơn giá.

        [JsonPropertyName("ProductID")]
        public int ProductId { get; set; } // ID sản phẩm, liên kết với Products.

        [JsonPropertyName("CreatedDate")]
        public DateTime CreatedDate { get; set; } // Ngày tạo topping, theo dõi thời gian tạo.
    }
}