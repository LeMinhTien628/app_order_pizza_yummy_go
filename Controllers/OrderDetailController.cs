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
    public class OrderDetailController : ControllerBase
    {
        private readonly AppOrderDbContext _context;

        public OrderDetailController(AppOrderDbContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetAll()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        // GET: api/OrderDetail/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDetail>> Get(int id)
        {
            var od = await _context.OrderDetails.FindAsync(id);
            if (od == null) return NotFound();
            return od;
        }

        // POST: api/OrderDetail
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> Create([FromBody] OrderDetail model)
        {
            _context.OrderDetails.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = model.OrderDetailId }, model);
        }

        // PUT: api/OrderDetail/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderDetail upd)
        {
            if (id != upd.OrderDetailId) return BadRequest();
            var od = await _context.OrderDetails.FindAsync(id);
            if (od == null) return NotFound();

            od.OrderId = upd.OrderId;
            od.ProductId = upd.ProductId;
            od.Quantity = upd.Quantity;
            od.Size = upd.Size;
            od.CrustType = upd.CrustType;
            od.UnitPrice = upd.UnitPrice;
            od.ToppingId = upd.ToppingId;
            od.ToppingPrice = upd.ToppingPrice;

            _context.Entry(od).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/OrderDetail/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var od = await _context.OrderDetails.FindAsync(id);
            if (od == null) return NotFound();
            _context.OrderDetails.Remove(od);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
