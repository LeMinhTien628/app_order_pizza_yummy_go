using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_app_pizza_flutter.Models;
using api_app_pizza_flutter.Data; // assuming DbContext is here

namespace api_app_pizza_flutter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AppOrderDbContext _context;

        public AddressController(AppOrderDbContext context)
        {
            _context = context;
        }

        // GET: api/Address
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            return await _context.Addresses.ToListAsync();
        }

        // GET: api/Address/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // POST: api/Address
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            // If setting default, unset others
            if (address.IsDefault)
            {
                var existingDefaults = await _context.Addresses
                    .Where(a => a.UserId == address.UserId && a.IsDefault)
                    .ToListAsync();
                existingDefaults.ForEach(a => a.IsDefault = false);
            }

            address.CreatedDate = DateTime.UtcNow;
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAddress), new { id = address.AddressId }, address);
        }

        // PUT: api/Address/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            if (id != address.AddressId)
            {
                return BadRequest();
            }

            // Handle default flag
            if (address.IsDefault)
            {
                var existingDefaults = await _context.Addresses
                    .Where(a => a.UserId == address.UserId && a.IsDefault && a.AddressId != id)
                    .ToListAsync();
                existingDefaults.ForEach(a => a.IsDefault = false);
            }

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Address/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(int id)
        {
            return _context.Addresses.Any(e => e.AddressId == id);
        }
    }
}
