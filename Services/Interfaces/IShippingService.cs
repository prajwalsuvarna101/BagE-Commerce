using Bag_E_Commerce.Models;

namespace Bag_E_Commerce.Services.Interfaces
{
    public interface IShippingService
    {
        Task<IEnumerable<ShippingModel>> GetAllShippingDetailsAsync();
        Task<ShippingModel?> GetShippingByIdAsync(int id);
        Task<ShippingModel> CreateShippingAsync(ShippingModel shipping);
        Task<ShippingModel> UpdateShippingAsync(int id, ShippingModel shipping);
        Task<bool> DeleteShippingAsync(int id);
    }
}
