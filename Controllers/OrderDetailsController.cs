using Microsoft.AspNetCore.Mvc;
using Bag_E_Commerce.Models;
using Bag_E_Commerce.Services;
using Bag_E_Commerce.Services.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;


namespace Bag_E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailsService _orderDetailsService;
        private readonly IOrderService _orderService;

        public OrderDetailsController(IOrderDetailsService orderDetailsService, IOrderService orderService)
        {
            _orderDetailsService = orderDetailsService;
            _orderService = orderService;
        }

        // Display all order details
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailsModel>>> GetAllOrderDetails()
        {
            var orderDetails = await _orderDetailsService.GetAllOrderDetailsAsync();
            return Ok(orderDetails);
        }

        // Display order details based on OrderId
        [HttpGet("order-items/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderDetailsModel>>> GetOrderDetailsByOrderId(int orderId)
        {
            var orderDetails = await _orderDetailsService.GetOrderDetailsByOrderIdAsync(orderId);

            if (orderDetails == null)
            {
                return NotFound("No order details found for the given order ID.");
            }

            return Ok(orderDetails);
        }

        // Delete order details by OrderId
        [HttpDelete("order/{orderId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteOrder(int orderId)
        {
            var order = await _orderService.DeleteOrderAsync(orderId);

            if (order == null)
            {
                return NotFound("Order not found.");
            }

            // Delete associated order details
            await _orderDetailsService.DeleteOrderDetailsByOrderIdAsync(orderId);

            return Ok("Order and associated order details deleted successfully.");
        }
    }
}
