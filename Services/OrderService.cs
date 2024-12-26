using Bag_E_Commerce.Data;
using Bag_E_Commerce.Models;
using Bag_E_Commerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bag_E_Commerce.Enums;

namespace Bag_E_Commerce.Services
{
    public class OrderService : IOrderService
    {
        private readonly BagDbContext _context;

        public OrderService(BagDbContext context)
        {
            _context = context;
        }

        // Get all orders
        public async Task<IEnumerable<OrderModel>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        // Get order by order ID
        public async Task<OrderModel> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .FirstOrDefaultAsync(order => order.OrderId == orderId);
        }

        // Get orders by user ID
        public async Task<IEnumerable<OrderModel>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders
                .Where(order => order.UserId == userId)
                .ToListAsync();
        }

        // Create a new order
        public async Task<OrderModel> CreateOrderAsync(OrderModel order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        // Update an existing order
        public async Task<OrderModel> UpdateOrderAsync(OrderModel order)
        {
            var existingOrder = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == order.OrderId);

            if (existingOrder != null)
            {
                existingOrder.TotalAmount = order.TotalAmount;
                existingOrder.OrderStatus = order.OrderStatus;
                existingOrder.CreatedAt = order.CreatedAt;

                _context.Orders.Update(existingOrder);
                await _context.SaveChangesAsync();
            }

            return existingOrder;
        }

        // Delete an order
        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<int> CheckoutAsync(int cartId)
        {
            // Get cart items
            var cartItems = await _context.Carts.Where(c => c.CartId == cartId).ToListAsync();
            if (!cartItems.Any())
                throw new Exception("Cart is empty!");

            // Calculate total amount
            var totalAmount = cartItems.Sum(item => item.Quantity * item.PricePerItem);

            // Get UserId from the first cart item (assuming all belong to the same user)
            var userId = cartItems.First().UserId;

            // Create a new order
            var order = new OrderModel
            {
                UserId = userId,
                TotalAmount = totalAmount,
                OrderStatus = OrderStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Transfer items to OrderDetails
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetailsModel
                {

                    OrderItemId = (int)(DateTime.UtcNow.Ticks % int.MaxValue),
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.PricePerItem
                };
                _context.OrderDetails.Add(orderDetail);
            }

            // Remove items from cart
            _context.Carts.RemoveRange(cartItems);

            // Save changes
            await _context.SaveChangesAsync();

            return order.OrderId; // Return the newly created order ID
        }

        public async Task<PaymentModel> CompletePayment(int orderId, PaymentMethod paymentMethod)
        {
            // Fetch the order
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);

            if (order == null)
            {
                throw new Exception($"Order with ID {orderId} not found.");
            }

            // Default payment status is set to Success
            var paymentStatus = PaymentStatus.Success;

            // Create a new payment entry
            var payment = new PaymentModel
            {
                OrderId = orderId,
                PaymentMethod = paymentMethod,
                PaymentStatus = paymentStatus,
                PaymentDate = DateTime.UtcNow
            };

            _context.Payment.Add(payment);
            await _context.SaveChangesAsync();

            return payment;
        }
    }
}
