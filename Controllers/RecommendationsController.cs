using api_app_pizza_flutter.Data;
using api_app_pizza_flutter.Models;
using api_app_pizza_flutter.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_app_pizza_flutter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationsController : ControllerBase
    {
        private readonly IRecommendationService _recommender;
        private readonly AppOrderDbContext _db;

        public RecommendationsController(IRecommendationService recommender, AppOrderDbContext db)
        {
            _recommender = recommender;
            _db = db;
        }

        // GET api/Recommendations/42
        [HttpGet("{productId:int}")]
        public async Task<ActionResult<IEnumerable<Product>>> Get(int productId)
        {
            // 1. Lấy danh sách ID gợi ý
            var recIds = _recommender.Recommend(productId, topN: 5);
            if (!recIds.Any()) return NoContent();

            // 2. Trả về chi tiết các sản phẩm
            var products = await _db.Products
                .Where(p => recIds.Contains(p.ProductId))
                .ToListAsync();

            return Ok(products);
        }
        // API: GET /api/recommendations/rules
        [HttpGet("rules")]
        public ActionResult<IEnumerable<object>> GetAllRules()
        {
            var result = _recommender.GetRules()
                .Select(rule => new {
                    Antecedent = rule.X,
                    Consequent = rule.Y,
                    Support = rule.Support,
                    Confidence = rule.Confidence
                });

            return Ok(result);
        }

    }
}
