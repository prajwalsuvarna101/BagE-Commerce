using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bag_E_Commerce.Models;
using Bag_E_Commerce.Services.Interfaces;

namespace Bag_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Category
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        // GET: api/Category/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound("Category not found.");
            return Ok(category);
        }

        // POST: api/Category
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryModel category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _categoryService.CreateCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, category);
        }

        // PUT: api/Category/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryModel category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _categoryService.UpdateCategoryAsync(id, category);
            if (updated!=null)
                return NotFound("Category not found.");

            return NoContent();
        }

        // DELETE: api/Category/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleted = await _categoryService.DeleteCategoryAsync(id);
            if (!deleted)
                return NotFound("Category not found.");

            return NoContent();
        }
    }
}
