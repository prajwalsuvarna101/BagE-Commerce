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
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BagModel>>> GetBags()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(new { message = "You must be authenticated to access the bags." });
            }

            if (!User.IsInRole("Admin") && !User.IsInRole("User"))
            {
                return Unauthorized(new { message = "You do not have the required role to access this resource." });
            }

            return await _context.Bags.ToListAsync();
        }

        // GET: api/Bag/5
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<BagModel>> GetBag(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(new { message = "You must be authenticated to access this bag." });
            }

            if (!User.IsInRole("Admin") && !User.IsInRole("User"))
            {
                return Unauthorized(new { message = "You do not have the required role to access this resource." });
            }

            var bag = await _context.Bags.FindAsync(id);

            if (bag == null)
            {
                return NotFound(new { message = "Bag not found." });
            }

            return bag;
        }

        // POST: api/Bag
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<BagModel>> PostBag(BagModel bag)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(new { message = "You must be authenticated to add a new bag." });
            }

            if (!User.IsInRole("Admin"))
            {
                return Unauthorized(new { message = "Only Admins can add new bags." });
            }

            _context.Bags.Add(bag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBag", new { id = bag.id }, bag);
        }

        // PUT: api/Bag/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBag(int id, BagModel bag)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(new { message = "You must be authenticated to update a bag." });
            }

            if (!User.IsInRole("Admin"))
            {
                return Unauthorized(new { message = "Only Admins can update bags." });
            }

            if (id != bag.id)
            {
                return BadRequest(new { message = "Bag ID mismatch." });
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
                    return NotFound(new { message = "Bag not found." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Bag/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBag(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(new { message = "You must be authenticated to delete a bag." });
            }

            if (!User.IsInRole("Admin"))
            {
                return Unauthorized(new { message = "Only Admins can delete bags." });
            }

            var bag = await _context.Bags.FindAsync(id);
            if (bag == null)
            {
                return NotFound(new { message = "Bag not found." });
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
