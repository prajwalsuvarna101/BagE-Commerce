using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bag_E_Commerce.Models;
using Bag_E_Commerce.Data;

namespace Bag_E_Commerce.Services
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly BagDbContext _context;

        public OrderDetailsService(BagDbContext context)
        {
            _context = context;
        }

        // Get all order details
        public async Task<IEnumerable<OrderDetailsModel>> GetAllOrderDetailsAsync()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        // Get order details by OrderId
        public async Task<IEnumerable<OrderDetailsModel>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            return await _context.OrderDetails
                                 .Where(od => od.OrderId == orderId)
                                 .ToListAsync();
        }

        // Delete order details by OrderId
        public async Task<int> DeleteOrderDetailsByOrderIdAsync(int orderId)
        {
            var orderDetails = await _context.OrderDetails
                                              .Where(od => od.OrderId == orderId)
                                              .ToListAsync();

            if (orderDetails.Any())
            {
                _context.OrderDetails.RemoveRange(orderDetails);
                return await _context.SaveChangesAsync();
            }

            return 0; // No order details found to delete
        }
    }
}
