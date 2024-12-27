using Bag_E_Commerce.Models;
using Bag_E_Commerce.Data;
using Bag_E_Commerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bag_E_Commerce.Services
{
    public class ShippingService : IShippingService
    {
        private readonly BagDbContext _context;

        public ShippingService(BagDbContext context)
        {
            _context = context;
        }

        // Get all shipping records
        public async Task<List<ShippingModel>> GetAllShippingAsync()
        {
            return await _context.Shipping.ToListAsync();
        }

        // Get shipping details by order ID
        public async Task<ShippingModel> GetShippingByOrderIdAsync(int orderId)
        {
            return await _context.Shipping
                .FirstOrDefaultAsync(s => s.OrderId == orderId);
        }

        // Update shipping status by order ID
        public async Task<ShippingModel> UpdateShippingStatusAsync(int orderId, ShippingStatus newStatus)
        {
            var shipping = await _context.Shipping
                .FirstOrDefaultAsync(s => s.OrderId == orderId);

            if (shipping == null)
            {
                return null; // Shipping record not found
            }

            shipping.ShippingStatus = newStatus;
            await _context.SaveChangesAsync();
            return shipping;
        }

        // Create shipping record
        public async Task<ShippingModel> CreateShippingAsync(ShippingModel shipping)
        {
            _context.Shipping.Add(shipping);
            await _context.SaveChangesAsync();
            return shipping;
        }
    }
}
