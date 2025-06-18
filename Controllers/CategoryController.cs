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
    public class CategoryController : ControllerBase
    {
        private readonly AppOrderDbContext _context;

        public CategoryController(AppOrderDbContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Category/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null) return NotFound();
            return cat;
        }

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<Category>> Create([FromBody] Category model)
        {
            _context.Categories.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = model.CategoryId }, model);
        }

        // PUT: api/Category/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category upd)
        {
            if (id != upd.CategoryId) return BadRequest();
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null) return NotFound();

            cat.CategoryName = upd.CategoryName;
            cat.CreatedDate = upd.CreatedDate;

            _context.Entry(cat).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Category/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null) return NotFound();
            _context.Categories.Remove(cat);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
