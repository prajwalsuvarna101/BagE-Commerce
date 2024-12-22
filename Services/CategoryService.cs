using Bag_E_Commerce.Data;
using Bag_E_Commerce.Models;
using Bag_E_Commerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bag_E_Commerce.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly BagDbContext _context;

        public CategoryService(BagDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<CategoryModel?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<CategoryModel> CreateCategoryAsync(CategoryModel category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<CategoryModel> UpdateCategoryAsync(int id, CategoryModel category)
        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null) throw new KeyNotFoundException("Category not found.");

            existingCategory.name = category.name;
            existingCategory.description = category.description;

            await _context.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
