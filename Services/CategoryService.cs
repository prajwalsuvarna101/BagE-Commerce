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

        // Get all categories
        public async Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        // Get a category by its ID
        public async Task<CategoryModel?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == id); // Assuming CategoryId is the key
        }

        // Create a new category
        public async Task<CategoryModel> CreateCategoryAsync(CategoryModel category)
        {
            _context.Categories.Add(category);  // Add the category to the DbContext
            await _context.SaveChangesAsync();  // Save the changes to the database
            return category;  // Return the created category
        }

        // Update an existing category
        public async Task<CategoryModel> UpdateCategoryAsync(int id, CategoryModel category)
        {
            var existingCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == id);  // Find the category by ID

            if (existingCategory == null)
            {
                return null;  // Return null if the category was not found
            }

            // Update properties of the existing category
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;

            _context.Categories.Update(existingCategory);  // Mark the category as modified
            await _context.SaveChangesAsync();  // Save changes to the database

            return existingCategory;  // Return the updated category
        }

        // Delete a category by its ID
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == id);  // Find the category by ID

            if (category == null)
            {
                return false;  // Return false if the category was not found
            }

            _context.Categories.Remove(category);  // Remove the category from the DbContext
            await _context.SaveChangesAsync();  // Save changes to the database

            return true;  // Return true if the category was successfully deleted
        }
    }
}
