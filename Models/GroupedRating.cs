namespace api_app_pizza_flutter.Models
{
    public class GroupedRating
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required List<double> Ratings { get; set; }
        public int RatingCount { get; set; }
        public double AverageRating { get; set; }
    }
}