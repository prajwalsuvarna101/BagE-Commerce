using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bag_E_Commerce.Models;
using Bag_E_Commerce.Data;

namespace Bag_E_Commerce.Services
{
    public class ReviewService : IReviewService
    {
        private readonly BagDbContext _context;

        public ReviewService(BagDbContext context)
        {
            _context = context;
        }

        // Create a review
        public async Task<ReviewModel> CreateReviewAsync(ReviewModel review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        // Get all reviews for a product
        public async Task<List<ReviewModel>> GetReviewsByProductAsync(int productId)
        {
            return await _context.Reviews
                .Where(r => r.ProductId == productId)
                .ToListAsync();
        }

        // Get a specific review by its ID
        public async Task<ReviewModel> GetReviewByIdAsync(int reviewId)
        {
            return await _context.Reviews
                .FirstOrDefaultAsync(r => r.ReviewId == reviewId);
        }

        // Get all reviews (new method to display all reviews)
        public async Task<List<ReviewModel>> GetAllReviewsAsync()
        {
            return await _context.Reviews
                .ToListAsync();
        }


        // Update a review
        public async Task<ReviewModel> UpdateReviewAsync(int reviewId, ReviewModel updatedReview)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review == null)
            {
                return null; // Or throw an exception if preferred
            }

            review.Rating = updatedReview.Rating;
            review.Comment = updatedReview.Comment;
            review.CreatedAt = updatedReview.CreatedAt; // Optionally update created_at

            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
            return review;
        }

        // Delete a review
        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review == null)
            {
                return false; // Or throw an exception if preferred
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
