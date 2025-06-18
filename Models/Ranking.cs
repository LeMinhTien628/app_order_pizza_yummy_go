using System.Text.Json.Serialization;

namespace api_app_pizza_flutter.Models
{
    // Ranking: Chức năng xếp hạng sản phẩm theo đánh giá.
    public class Ranking
    {
        [JsonPropertyName("RankingID")]
        public int RankingId { get; set; } // ID xếp hạng, khóa chính.

        [JsonPropertyName("ProductID")]
        public int ProductId { get; set; } // ID sản phẩm, liên kết với Products.

        [JsonPropertyName("AverageRating")]
        public decimal AverageRating { get; set; } // Điểm đánh giá trung bình, từ 0-5.

        [JsonPropertyName("RankPosition")]
        public int RankPosition { get; set; } // Vị trí xếp hạng, hiển thị thứ tự.

        [JsonPropertyName("Period")]
        public string Period { get; set; } = string.Empty; // Chu kỳ xếp hạng (Weekly, Monthly).

        [JsonPropertyName("CreatedDate")]
        public DateTime CreatedDate { get; set; } // Ngày tạo xếp hạng, theo dõi thời gian.
    }
}