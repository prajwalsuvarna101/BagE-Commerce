using Bag_E_Commerce.Data;
using Bag_E_Commerce.Models;
using Bag_E_Commerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bag_E_Commerce.Services
{
    public class BagService : IBagService
    {
        private readonly BagDbContext _context;

        public BagService(BagDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BagModel>> GetAllBagsAsync()
        {
            return await _context.Bags.ToListAsync();
        }

        public async Task<BagModel?> GetBagByIdAsync(int id)
        {
            return await _context.Bags.FindAsync(id);
        }

        public async Task<BagModel> CreateBagAsync(BagModel bag)
        {
            _context.Bags.Add(bag);
            await _context.SaveChangesAsync();
            return bag;
        }

        public async Task<BagModel> UpdateBagAsync(int id, BagModel bag)
        {
            var existingBag = await _context.Bags.FindAsync(id);
            if (existingBag == null) throw new KeyNotFoundException("Bag not found.");

            existingBag.name = bag.name;
            existingBag.description = bag.description;
            existingBag.price = bag.price;
            existingBag.quantity = bag.quantity;

            await _context.SaveChangesAsync();
            return existingBag;
        }

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
