using Bag_E_Commerce.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bag_E_Commerce.Services.Interfaces
{
    public interface IShippingService
    {
        Task<List<ShippingModel>> GetAllShippingAsync();
        Task<ShippingModel> GetShippingByOrderIdAsync(int orderId);
        Task<ShippingModel> UpdateShippingStatusAsync(int orderId, ShippingStatus newStatus);
        Task<ShippingModel> CreateShippingAsync(ShippingModel shipping);
    }
}
