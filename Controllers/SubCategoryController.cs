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
    public class SubCategoryController : ControllerBase
    {
        private readonly AppOrderDbContext _context;

        public SubCategoryController(AppOrderDbContext context)
        {
            _context = context;
        }

        // GET: api/SubCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategory>>> GetAll()
        {
            return await _context.SubCategories.ToListAsync();
        }

        // GET: api/SubCategory/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SubCategory>> Get(int id)
        {
            var sc = await _context.SubCategories.FindAsync(id);
            if (sc == null) return NotFound();
            return sc;
        }

        // POST: api/SubCategory
        [HttpPost]
        public async Task<ActionResult<SubCategory>> Create([FromBody] SubCategory model)
        {
            model.CreatedDate = DateTime.UtcNow;
            _context.SubCategories.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = model.SubCategoryId }, model);
        }

        // PUT: api/SubCategory/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] SubCategory upd)
        {
            if (id != upd.SubCategoryId) return BadRequest();
            var sc = await _context.SubCategories.FindAsync(id);
            if (sc == null) return NotFound();

            sc.CategoryId = upd.CategoryId;
            sc.SubCategoryName = upd.SubCategoryName;
            // giữ nguyên CreatedDate

            _context.Entry(sc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/SubCategory/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sc = await _context.SubCategories.FindAsync(id);
            if (sc == null) return NotFound();
            _context.SubCategories.Remove(sc);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
