using Bag_E_Commerce.Models;

namespace Bag_E_Commerce.Services.Interfaces
{
    public interface IVendorService
    {
        Task<IEnumerable<VendorModel>> GetAllVendorsAsync();
        Task<VendorModel?> GetVendorByIdAsync(int id);
        Task<VendorModel> CreateVendorAsync(VendorModel vendor);
        Task<VendorModel> UpdateVendorAsync(int id, VendorModel vendor);
        Task<bool> DeleteVendorAsync(int id);
    }
}
