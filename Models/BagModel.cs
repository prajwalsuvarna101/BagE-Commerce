using System.ComponentModel.DataAnnotations;

public class BagModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int CategoryId { get; set; } // Foreign key for Category

    [Required]
    public int VendorId { get; set; } // Foreign key for Vendor
}
