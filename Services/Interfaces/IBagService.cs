using Bag_E_Commerce.Models;

namespace Bag_E_Commerce.Services.Interfaces
{
    public interface IBagService
    {
        Task<IEnumerable<BagModel>> GetAllBagsAsync();
        Task<BagModel?> GetBagByIdAsync(int id);
        Task<BagModel> CreateBagAsync(BagModel bag);
        Task<BagModel> UpdateBagAsync(int id, BagModel bag);
        Task<bool> DeleteBagAsync(int id);
    }
}
