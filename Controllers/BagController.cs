using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bag_E_Commerce.Data;
using Bag_E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Bag_E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BagController : ControllerBase
    {
        private readonly BagDbContext _context;

        public BagController(BagDbContext context)
        {
            _context = context;
        }

        // GET: api/Bag
        [HttpGet]
        [Authorize]  // Protect this endpoint with authentication
        public async Task<ActionResult<IEnumerable<BagModel>>> GetBags()
        {
            return await _context.Bags.ToListAsync();
        }

        // GET: api/Bag/5
        [HttpGet("{id}")]
        [Authorize]  // Protect this endpoint with authentication
        public async Task<ActionResult<BagModel>> GetBag(int id)
        {
            var bag = await _context.Bags.FindAsync(id);

            if (bag == null)
            {
                return NotFound();
            }

            return bag;
        }

        // POST: api/Bag
        [HttpPost]
        [Authorize]  // Protect this endpoint with authentication
        public async Task<ActionResult<BagModel>> PostBag(BagModel bag)
        {
            _context.Bags.Add(bag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBag", new { id = bag.id }, bag);
        }

        // PUT: api/Bag/5
        [HttpPut("{id}")]
        [Authorize]  // Protect this endpoint with authentication
        public async Task<IActionResult> PutBag(int id, BagModel bag)
        {
            if (id != bag.id)
            {
                return BadRequest();
            }

            _context.Entry(bag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BagExists(id))
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

        // DELETE: api/Bag/5
        [HttpDelete("{id}")]
        [Authorize]  // Protect this endpoint with authentication
        public async Task<IActionResult> DeleteBag(int id)
        {
            var bag = await _context.Bags.FindAsync(id);
            if (bag == null)
            {
                return NotFound();
            }

            _context.Bags.Remove(bag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BagExists(int id)
        {
            return _context.Bags.Any(e => e.id == id);
        }
    }
}
