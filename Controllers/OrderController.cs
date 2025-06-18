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
    public class OrderController : ControllerBase
    {
        private readonly AppOrderDbContext _context;

        public OrderController(AppOrderDbContext context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Order/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();
            return order;
        }

        // POST: api/Order
        [HttpPost]
        public async Task<ActionResult<Order>> Create([FromBody] Order model)
        {
            _context.Orders.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = model.OrderId }, model);
        }

        // PUT: api/Order/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Order upd)
        {
            if (id != upd.OrderId) return BadRequest();
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            order.UserId = upd.UserId;
            order.AddressId = upd.AddressId;
            order.OrderDate = upd.OrderDate;
            order.TotalAmount = upd.TotalAmount;
            order.Status = upd.Status;
            order.PaymentMethod = upd.PaymentMethod;
            order.ShippingFee = upd.ShippingFee;
            order.PromotionId = upd.PromotionId;
            order.CreatedDate = upd.CreatedDate;

            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Order/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
