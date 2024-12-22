using Bag_E_Commerce.Models;

namespace Bag_E_Commerce.Services.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewModel>> GetAllReviewsAsync();
        Task<ReviewModel?> GetReviewByIdAsync(int id);
        Task<ReviewModel> CreateReviewAsync(ReviewModel review);
        Task<ReviewModel> UpdateReviewAsync(int id, ReviewModel review);
        Task<bool> DeleteReviewAsync(int id);
    }
}
