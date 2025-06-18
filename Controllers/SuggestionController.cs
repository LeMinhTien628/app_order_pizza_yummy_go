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
    public class SuggestionController : ControllerBase
    {
        private readonly AppOrderDbContext _context;

        public SuggestionController(AppOrderDbContext context)
        {
            _context = context;
        }

        // GET: api/Suggestion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Suggestion>>> GetAll()
        {
            return await _context.Suggestions.ToListAsync();
        }

        // GET: api/Suggestion/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Suggestion>> Get(int id)
        {
            var s = await _context.Suggestions.FindAsync(id);
            if (s == null) return NotFound();
            return s;
        }

        // POST: api/Suggestion
        [HttpPost]
        public async Task<ActionResult<Suggestion>> Create([FromBody] Suggestion model)
        {
            model.CreatedDate = DateTime.UtcNow;
            _context.Suggestions.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = model.SuggestionId }, model);
        }

        // PUT: api/Suggestion/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Suggestion upd)
        {
            if (id != upd.SuggestionId) return BadRequest();
            var s = await _context.Suggestions.FindAsync(id);
            if (s == null) return NotFound();

            s.ProductId = upd.ProductId;
            s.SuggestedProductId = upd.SuggestedProductId;
            s.Confidence = upd.Confidence;
            // giữ nguyên CreatedDate

            _context.Entry(s).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Suggestion/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var s = await _context.Suggestions.FindAsync(id);
            if (s == null) return NotFound();
            _context.Suggestions.Remove(s);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
