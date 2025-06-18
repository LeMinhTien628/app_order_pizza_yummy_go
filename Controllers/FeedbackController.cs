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
    public class FeedbackController : ControllerBase
    {
        private readonly AppOrderDbContext _context;

        public FeedbackController(AppOrderDbContext context)
        {
            _context = context;
        }

        // GET: api/Feedback
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetAll()
        {
            return await _context.Feedbacks.ToListAsync();
        }

        // GET: api/Feedback/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Feedback>> Get(int id)
        {
            var fb = await _context.Feedbacks.FindAsync(id);
            if (fb == null) return NotFound();
            return fb;
        }

        // POST: api/Feedback
        [HttpPost]
        public async Task<ActionResult<Feedback>> Create([FromBody] Feedback model)
        {
            _context.Feedbacks.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = model.FeedbackId }, model);
        }

        // PUT: api/Feedback/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Feedback upd)
        {
            if (id != upd.FeedbackId) return BadRequest();
            var fb = await _context.Feedbacks.FindAsync(id);
            if (fb == null) return NotFound();

            fb.ProductId = upd.ProductId;
            fb.OrderId = upd.OrderId;
            fb.UserId = upd.UserId;
            fb.CrustRating = upd.CrustRating;
            fb.SauceRating = upd.SauceRating;
            fb.CheeseRating = upd.CheeseRating;
            fb.ToppingRating = upd.ToppingRating;
            fb.OverallTasteRating = upd.OverallTasteRating;
            fb.PresentationRating = upd.PresentationRating;
            fb.ServiceRating = upd.ServiceRating;
            fb.Comment = upd.Comment;
            fb.CreatedDate = upd.CreatedDate;

            _context.Entry(fb).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Feedback/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var fb = await _context.Feedbacks.FindAsync(id);
            if (fb == null) return NotFound();
            _context.Feedbacks.Remove(fb);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
