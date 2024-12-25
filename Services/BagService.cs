using Bag_E_Commerce.Data;
using Bag_E_Commerce.Models;
using Bag_E_Commerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bag_E_Commerce.Services
{
    public class BagService : IBagService
    {
        private readonly BagDbContext _context;

        public BagService(BagDbContext context)
        {
            _context = context;
        }

        // Get all bags with related Category and Vendor information
        public async Task<IEnumerable<object>> GetAllBagsAsync()
        {
            var bags = await _context.Bags.ToListAsync();
            var result = new List<object>();

            foreach (var bag in bags)
            {
                var category = await _context.Categories.FindAsync(bag.CategoryId);
                var vendor = await _context.Vendors.FindAsync(bag.VendorId);

                var bagWithDetails = new
                {
                    bag.Id,
                    bag.Name,
                    bag.Description,
                    bag.Price,
                    Category = new
                    {
                        category?.CategoryId,
                        category?.Name,
                        category?.Description
                    },
                    Vendor = new
                    {
                        vendor?.VendorId,
                        vendor?.Name,
                        vendor?.ContactDetails,
                        vendor?.CreatedAt
                    }
                };

                result.Add(bagWithDetails);
            }

            return result;
        }

        // Get a single bag by id with related Category and Vendor information
        public async Task<object?> GetBagByIdAsync(int id)
        {
            var bag = await _context.Bags.FindAsync(id);
            if (bag == null) return null;

            var category = await _context.Categories.FindAsync(bag.CategoryId);
            var vendor = await _context.Vendors.FindAsync(bag.VendorId);

            var result = new
            {
                bag.Id,
                bag.Name,
                bag.Description,
                bag.Price,
                Category = new
                {
                    category?.CategoryId,
                    category?.Name,
                    category?.Description
                },
                Vendor = new
                {
                    vendor?.VendorId,
                    vendor?.Name,
                    vendor?.ContactDetails,
                    vendor?.CreatedAt
                }
            };

            return result;
        }

        // Create a new bag
        public async Task<BagModel> CreateBagAsync(BagModel bag)
        {
            _context.Bags.Add(bag);
            await _context.SaveChangesAsync();
            return bag;
        }

        // Update an existing bag
        public async Task<BagModel> UpdateBagAsync(int id, BagModel bag)
        {
            var existingBag = await _context.Bags.FindAsync(id);
            if (existingBag == null) throw new KeyNotFoundException("Bag not found.");

            existingBag.Name = bag.Name;
            existingBag.Description = bag.Description;
            existingBag.Price = bag.Price;
            existingBag.CategoryId = bag.CategoryId;
            existingBag.VendorId = bag.VendorId;

            await _context.SaveChangesAsync();
            return existingBag;
        }

        // Delete a bag
        public async Task<bool> DeleteBagAsync(int id)
        {
            var bag = await _context.Bags.FindAsync(id);
            if (bag == null) return false;

            _context.Bags.Remove(bag);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
