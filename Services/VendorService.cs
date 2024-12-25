using Bag_E_Commerce.Data;
using Bag_E_Commerce.Models;
using Bag_E_Commerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

public class VendorService : IVendorService
{
    private readonly BagDbContext _dbContext;

    public VendorService(BagDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<VendorModel>> GetAllVendorsAsync()
    {
        return await _dbContext.Vendors.ToListAsync();
    }

    public async Task<VendorModel?> GetVendorByIdAsync(int id)
    {
        return await _dbContext.Vendors.FindAsync(id);
    }

    public async Task<VendorModel> CreateVendorAsync(VendorModel vendor)
    {
        _dbContext.Vendors.Add(vendor);
        await _dbContext.SaveChangesAsync();
        return vendor;
    }

    public async Task<VendorModel?> UpdateVendorAsync(int id, VendorModel vendor)
    {
        var existingVendor = await _dbContext.Vendors.FindAsync(id);
        if (existingVendor == null)
        {
            return null;
        }

        existingVendor.Name = vendor.Name;
        existingVendor.ContactDetails = vendor.ContactDetails;

        _dbContext.Vendors.Update(existingVendor);
        await _dbContext.SaveChangesAsync();

        return existingVendor;
    }

    public async Task<bool> DeleteVendorAsync(int id)
    {
        var vendor = await _dbContext.Vendors.FindAsync(id);
        if (vendor == null)
        {
            return false;
        }

        _dbContext.Vendors.Remove(vendor);
        await _dbContext.SaveChangesAsync();

        return true;
    }


}
