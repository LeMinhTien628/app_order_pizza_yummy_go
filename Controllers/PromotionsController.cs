using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api_app_pizza_flutter.Data;
using api_app_pizza_flutter.Models;
using Microsoft.EntityFrameworkCore;

namespace api_app_pizza_flutter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly AppOrderDbContext _context;
        public PromotionsController(AppOrderDbContext context)
        {
            _context = context;
        }
        // GET: api/Promotions
        [HttpGet]
        public IActionResult GetPromotions()
        {
            try
            {
                var promotions = _context.Promotions.ToList();
                return Ok(promotions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
        // GET: api/Promotions/5
        [HttpGet("{id}")]
        public IActionResult GetPromotion(int id)
        {
            try
            {
                var promotion = _context.Promotions.Find(id);
                if (promotion == null)
                {
                    return NotFound($"Không tìm thấy khuyến mãi với ID {id}");
                }
                return Ok(promotion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
    }
}
