using System.Text.Json.Serialization;

namespace api_app_pizza_flutter.Models
{
    // OrderDetail: Chức năng quản lý chi tiết sản phẩm trong đơn hàng.
    public class OrderDetail
    {
        [JsonPropertyName("OrderDetailID")]
        public int OrderDetailId { get; set; } // ID chi tiết đơn hàng, khóa chính.

        [JsonPropertyName("OrderID")]
        public int OrderId { get; set; } // ID đơn hàng, liên kết với Orders.

        [JsonPropertyName("ProductID")]
        public int ProductId { get; set; } // ID sản phẩm, liên kết với Products.

        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; } // Số lượng sản phẩm, dùng để tính giá.

        [JsonPropertyName("Size")]
        public string? Size { get; set; } // Kích thước sản phẩm (ví dụ: Nhỏ, Lớn), có thể null.

        [JsonPropertyName("CrustType")]
        public string? CrustType { get; set; } // Loại đế (ví dụ: Dày, Mỏng), có thể null.

        [JsonPropertyName("UnitPrice")]
        public decimal UnitPrice { get; set; } // Giá đơn vị sản phẩm, dùng để tính giá.

        [JsonPropertyName("ToppingID")]
        public int? ToppingId { get; set; } // ID topping, liên kết với Toppings, có thể null.

        [JsonPropertyName("ToppingPrice")]
        public decimal ToppingPrice { get; set; } // Giá topping, dùng để tính giá.

    }
}