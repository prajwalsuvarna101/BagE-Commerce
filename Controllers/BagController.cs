using Microsoft.AspNetCore.Authorization;
using Bag_E_Commerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bag_E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BagController : ControllerBase
    {
        private readonly IBagService _bagService;

        public BagController(IBagService bagService)
        {
            _bagService = bagService;
        }

        // GET: api/Bag
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<object>>> GetBags()
        {
            var bags = await _bagService.GetAllBagsAsync();

            if (bags == null || !bags.Any())
            {
                return NotFound(new { message = "No bags found." });
            }

            return Ok(bags); // Return the bags with populated Category and Vendor data
        }

        // GET: api/Bag/id
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<object>> GetBag(int id)
        {
            var bag = await _bagService.GetBagByIdAsync(id);

            if (bag == null)
            {
                return NotFound(new { message = "Bag not found." });
            }

            return Ok(bag); // Return the single bag with populated Category and Vendor data
        }
        // GET: api/Bag/category/{categoryId}
        [HttpGet("category/{categoryId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<object>>> GetBagsByCategory(int categoryId)
        {
            // Fetch the bags that belong to the specified category
            var bags = await _bagService.GetBagsByCategoryIdAsync(categoryId);

            if (bags == null || !bags.Any())
            {
                return NotFound(new { message = "No bags found for the specified category." });
            }

            return Ok(bags);
        }

        // GET: api/Bag/vendor/{vendorId}
        [HttpGet("vendor/{VendorId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<object>>> GetBagsByVendor(int VendorId)
        {
            // Fetch the bags that belong to the specified category
            var bags = await _bagService.GetBagsByCategoryIdAsync(VendorId);

            if (bags == null || !bags.Any())
            {
                return NotFound(new { message = "No bags found for the specified Vendor." });
            }
            return Ok(bags);
        }

        // POST: api/Bag
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BagModel>> PostBag(BagModel bag)
        {
            var createdBag = await _bagService.CreateBagAsync(bag);
            return CreatedAtAction(nameof(GetBag), new { id = createdBag.Id }, createdBag);
        }

        // PUT: api/Bag/id
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutBag(int id, BagModel bag)
        {
            var updatedBag = await _bagService.UpdateBagAsync(id, bag);
            if (updatedBag == null)
            {
                return NotFound(new { message = "Bag not found." });
            }

            return NoContent();
        }

        // DELETE: api/Bag/id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBag(int id)
        {
            var result = await _bagService.DeleteBagAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Bag not found." });
            }

            return NoContent();
        }
    }
}
