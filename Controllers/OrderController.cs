﻿using Bag_E_Commerce.Models;
using Bag_E_Commerce.Services.Interfaces;
using Bag_E_Commerce.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Bag_E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IShippingService _shippingService;

        public OrderController(IOrderService orderService, IShippingService shippingService)
        {
            _orderService = orderService;
            _shippingService = shippingService;
        }

        // Get all orders
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // Get order by ID
        [HttpGet("{orderId}")]
        [Authorize(Roles = "Admin,User")]
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
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetOrdersByUserId(int userId)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }

        // Create a new order
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<OrderModel>> CreateOrder([FromBody] OrderModel order)
        {
            var createdOrder = await _orderService.CreateOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { orderId = createdOrder.OrderId }, createdOrder);
        }

        // Cancel order
        [HttpPut("/Order/cancel/{orderId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<OrderModel>> UpdateOrder(int orderId, OrderStatus orderStatus)
        {
            var existingOrder = await _orderService.GetOrderByIdAsync(orderId);

            if (existingOrder == null)
            {
                return NotFound("Order not found.");
            }

            if (existingOrder.OrderStatus == OrderStatus.Cancelled)
            {
                return BadRequest("Order has already been canceled and cannot be modified.");
            }
            if (orderStatus != OrderStatus.Cancelled)
            {
                return BadRequest("Users can only cancel the order.");
            }

            if (orderStatus == OrderStatus.Cancelled)
            {
                existingOrder.OrderStatus = OrderStatus.Cancelled;
            }
            else
            {
                return BadRequest("Invalid status change.");
            }

            var updatedOrder = await _orderService.UpdateOrderAsync(existingOrder);
            if (updatedOrder == null)
            {
                return NotFound("Failed to update the order.");
            }

            return Ok(existingOrder);
        }

        // Delete an order
        [HttpDelete("{orderId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var success = await _orderService.DeleteOrderAsync(orderId);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("checkout")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Checkout(int cartId)
        {
            try
            {
                var orderId = await _orderService.CheckoutAsync(cartId);
                return Ok(new { OrderId = orderId, Message = "Checkout successful!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("{orderId}/complete-payment")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> CompletePayment(int orderId, [FromBody] PaymentRequest request)
        {
            try
            {
                // Call the service to complete the payment
                var payment = await _orderService.CompletePayment(orderId, request.PaymentMethod);

                var shipping = new ShippingModel
                {
                    OrderId = orderId,
                    ShippingAddress = request.ShippingAddress == null ? "HOME" : request.ShippingAddress,  // Assuming this is part of the payment request
                    ShippingStatus = ShippingStatus.Pending
                };

                // Create the shipping record
                var createdShipping = await _shippingService.CreateShippingAsync(shipping);

                // Return the payment object in the response
                return Ok(new
                {
                    Message = "Payment completed successfully.Ready to be shipped",
                    Payment = payment,
                    Shipping = createdShipping
                });
            }
            catch (Exception ex)
            {
                // Return a BadRequest response with the error message
                return BadRequest(new { Message = ex.Message });
            }
        }

    }
}
