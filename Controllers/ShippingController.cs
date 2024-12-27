using Bag_E_Commerce.Models;
using Bag_E_Commerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Bag_E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly IShippingService _shippingService;

        public ShippingController(IShippingService shippingService)
        {
            _shippingService = shippingService;
        }

        // Get all shipping records
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<ShippingModel>>> GetAllShipping()
        {
            var shippingRecords = await _shippingService.GetAllShippingAsync();
            return Ok(shippingRecords);
        }

        // Get shipping info by order ID
        [HttpGet("{orderId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<ShippingModel>> GetShippingByOrderId(int orderId)
        {
            var shipping = await _shippingService.GetShippingByOrderIdAsync(orderId);

            if (shipping == null)
            {
                return NotFound(new { Message = "Shipping not found for the given order ID" });
            }

            return Ok(shipping);
        }

        // Update shipping status by order ID
        [HttpPut("{orderId}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ShippingModel>> UpdateShippingStatus(int orderId, [FromBody] ShippingStatus status)
        {
            var updatedShipping = await _shippingService.UpdateShippingStatusAsync(orderId, status);

            if (updatedShipping == null)
            {
                return NotFound(new { Message = "Shipping record not found for the given order ID" });
            }

            return Ok(new
            {
                Message = "Shipping status updated successfully",
                Shipping = updatedShipping
            });
        }
    }
}
