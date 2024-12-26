using Bag_E_Commerce.Data;
using Bag_E_Commerce.Models;
using Bag_E_Commerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
