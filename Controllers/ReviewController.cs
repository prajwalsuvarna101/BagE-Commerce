using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bag_E_Commerce.Models;
using Bag_E_Commerce.Services;
using Microsoft.AspNetCore.Authorization;

namespace Bag_E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        // Constructor to inject the ReviewService
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // Create a review
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> CreateReview([FromBody] ReviewModel review)
        {
            if (review == null)
            {
                return BadRequest("Invalid review data.");
            }

            var createdReview = await _reviewService.CreateReviewAsync(review);
            return CreatedAtAction(nameof(GetReviewById), new { reviewId = createdReview.ReviewId }, createdReview);
        }

        // Get all reviews for a product
        [HttpGet("product/{productId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<List<ReviewModel>>> GetReviewsByProduct(int productId)
        {
            var reviews = await _reviewService.GetReviewsByProductAsync(productId);
            if (reviews == null || reviews.Count == 0)
            {
                return NotFound("No reviews found for this product.");
            }

            return Ok(reviews);
        }

        // Get a specific review by its ID
        [HttpGet("{reviewId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<ReviewModel>> GetReviewById(int reviewId)
        {
            var review = await _reviewService.GetReviewByIdAsync(reviewId);
            if (review == null)
            {
                return NotFound("Review not found.");
            }

            return Ok(review);
        }

        // Update a review
        [HttpPut("{reviewId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> UpdateReview(int reviewId, [FromBody] ReviewModel updatedReview)
        {
            if (updatedReview == null)
            {
                return BadRequest("Invalid review data.");
            }

            var review = await _reviewService.UpdateReviewAsync(reviewId, updatedReview);
            if (review == null)
            {
                return NotFound("Review not found.");
            }

            return Ok(review);
        }

        // Get all reviews (new method to display all reviews)
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<List<ReviewModel>>> GetAllReviews()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            if (reviews == null || reviews.Count == 0)
            {
                return NotFound("No reviews found.");
            }

            return Ok(reviews);
        }

        // Delete a review
        [HttpDelete("{reviewId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var result = await _reviewService.DeleteReviewAsync(reviewId);
            if (!result)
            {
                return NotFound("Review not found.");
            }

            return NoContent();
        }
    }
}
