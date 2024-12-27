using Bag_E_Commerce.Models;
using Bag_E_Commerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class VendorsController : ControllerBase
{
    private readonly IVendorService _vendorService;

    public VendorsController(IVendorService vendorService)
    {
        _vendorService = vendorService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> GetAllVendors()
    {
        var vendors = await _vendorService.GetAllVendorsAsync();
        return Ok(vendors);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> GetVendorById(int id)
    {
        var vendor = await _vendorService.GetVendorByIdAsync(id);
        if (vendor == null)
        {
            return NotFound();
        }
        return Ok(vendor);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateVendor([FromBody] VendorModel vendor)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdVendor = await _vendorService.CreateVendorAsync(vendor);
        return CreatedAtAction(nameof(GetVendorById), new { id = createdVendor.VendorId }, createdVendor);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateVendor(int id, [FromBody] VendorModel vendor)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedVendor = await _vendorService.UpdateVendorAsync(id, vendor);
        if (updatedVendor == null)
        {
            return NotFound();
        }

        return Ok(updatedVendor);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteVendor(int id)
    {
        var success = await _vendorService.DeleteVendorAsync(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
