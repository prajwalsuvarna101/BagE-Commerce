using Bag_E_Commerce.Models;

namespace Bag_E_Commerce.Services.Interfaces
{
    public interface IBagService
    {    
        // Returns a collection of bags with populated Category and Vendor data
        Task<IEnumerable<object>> GetAllBagsAsync();

        // Returns a single bag with populated Category and Vendor data
        Task<object?> GetBagByIdAsync(int id);

        //Get the Bags related to a specific Category
        Task<IEnumerable<BagModel>> GetBagsByCategoryIdAsync(int categoryId);
        
        //Get the Bags related to a specific Vendor
        Task<IEnumerable<BagModel>> GetBagsByVendorIdAsync(int vendorId);

        // Creates a new bag and returns the created bag with its details
        Task<BagModel> CreateBagAsync(BagModel bag);

        // Updates an existing bag and returns the updated bag
        Task<BagModel> UpdateBagAsync(int id, BagModel bag);

        // Deletes a bag by its ID and returns a boolean indicating success
        Task<bool> DeleteBagAsync(int id);
    }
}
