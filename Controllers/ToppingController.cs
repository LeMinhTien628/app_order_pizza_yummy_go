using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_app_pizza_flutter.Data;
using api_app_pizza_flutter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_app_pizza_flutter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToppingController : ControllerBase
    {
        private readonly AppOrderDbContext _context;

        public ToppingController(AppOrderDbContext context)
        {
            _context = context;
        }

        // GET: api/Topping
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Topping>>> GetToppings()
        {
            return await _context.Toppings.ToListAsync();
        }

        // GET: api/Topping/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Topping>> GetTopping(int id)
        {
            var topping = await _context.Toppings.FindAsync(id);
            if (topping == null)
                return NotFound();
            return topping;
        }

        // POST: api/Topping
        [HttpPost]
        public async Task<ActionResult<Topping>> CreateTopping([FromBody] Topping topping)
        {
            topping.CreatedDate = DateTime.UtcNow;
            _context.Toppings.Add(topping);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTopping), new { id = topping.ToppingId }, topping);
        }

        // PUT: api/Topping/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTopping(int id, [FromBody] Topping updated)
        {
            if (id != updated.ToppingId)
                return BadRequest();

            var topping = await _context.Toppings.FindAsync(id);
            if (topping == null)
                return NotFound();

            topping.ToppingName = updated.ToppingName;
            topping.Price = updated.Price;
            topping.ProductId = updated.ProductId;
            // không đổi CreatedDate

            _context.Entry(topping).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Topping/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTopping(int id)
        {
            var topping = await _context.Toppings.FindAsync(id);
            if (topping == null)
                return NotFound();

            _context.Toppings.Remove(topping);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
