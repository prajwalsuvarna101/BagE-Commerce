using Bag_E_Commerce.Models;
using Bag_E_Commerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bag_E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Get all orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // Get order by ID
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderModel>> GetOrderById(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // Get orders by user ID
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetOrdersByUserId(int userId)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }

        // Create a new order
        [HttpPost]
        public async Task<ActionResult<OrderModel>> CreateOrder([FromBody] OrderModel order)
        {
            var createdOrder = await _orderService.CreateOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { orderId = createdOrder.OrderId }, createdOrder);
        }

        // Update an existing order
        [HttpPut("{orderId}")]
        public async Task<ActionResult<OrderModel>> UpdateOrder(int orderId, [FromBody] OrderModel order)
        {
            if (orderId != order.OrderId)
            {
                return BadRequest();
            }

            var updatedOrder = await _orderService.UpdateOrderAsync(order);
            if (updatedOrder == null)
            {
                return NotFound();
            }

            return Ok(updatedOrder);
        }

        // Delete an order
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var success = await _orderService.DeleteOrderAsync(orderId);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
