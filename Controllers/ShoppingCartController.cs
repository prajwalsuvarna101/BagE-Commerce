using Bag_E_Commerce.Models;
using Bag_E_Commerce.Services;
using Bag_E_Commerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bag_E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        // GET: api/ShoppingCart/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserCart(int userId)
        {
            var cartItems = await _shoppingCartService.GetUserCartAsync(userId);
            if (cartItems == null || !cartItems.Any())
            {
                return NotFound("No items found in the cart.");
            }
            return Ok(cartItems);
        }

        // POST: api/ShoppingCart
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] ShoppingCartModel shoppingCart)
        {
            if (shoppingCart == null)
            {
                return BadRequest("Invalid shopping cart data.");
            }

            var addedItem = await _shoppingCartService.AddToCartAsync(shoppingCart);
            return CreatedAtAction(nameof(GetUserCart), new { userId = shoppingCart.UserId }, addedItem);
        }

        // PUT: api/ShoppingCart
        [HttpPut]
        public async Task<IActionResult> UpdateCart([FromBody] ShoppingCartModel shoppingCart)
        {
            if (shoppingCart == null)
            {
                return BadRequest("Invalid shopping cart data.");
            }

            var updatedItem = await _shoppingCartService.UpdateCartAsync(shoppingCart);
            if (updatedItem == null)
            {
                return NotFound("Cart item not found.");
            }

            return Ok(updatedItem);
        }

        // DELETE: api/ShoppingCart/{userId}/{productId}
        [HttpDelete("{userId}/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int userId, int productId)
        {
            var isRemoved = await _shoppingCartService.RemoveFromCartAsync(userId, productId);
            if (!isRemoved)
            {
                return NotFound("Cart item not found.");
            }

            return NoContent(); // Return 204 No Content if deletion was successful
        }

        // GET: api/ShoppingCart/Total/{userId}
        [HttpGet("Total/{userId}")]
        public async Task<IActionResult> GetTotalPrice(int userId)
        {
            var totalPrice = await _shoppingCartService.GetTotalPriceAsync(userId);
            return Ok(new { TotalPrice = totalPrice });
        }
    }
}
