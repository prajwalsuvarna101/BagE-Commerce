using System.Collections.Generic;
using System.Threading.Tasks;
using Bag_E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Bag_E_Commerce.Services
{
    public interface IReviewService
    {
        // Create a review
        Task<ReviewModel> CreateReviewAsync(ReviewModel review);

        // Get all reviews (new method to display all reviews)
        Task<List<ReviewModel>> GetAllReviewsAsync();
        

        // Get all reviews for a product
        Task<List<ReviewModel>> GetReviewsByProductAsync(int productId);

        // Get a specific review by its ID
        Task<ReviewModel> GetReviewByIdAsync(int reviewId);

        // Update a review
        Task<ReviewModel> UpdateReviewAsync(int reviewId, ReviewModel updatedReview);

        // Delete a review
        Task<bool> DeleteReviewAsync(int reviewId);
    }
}
