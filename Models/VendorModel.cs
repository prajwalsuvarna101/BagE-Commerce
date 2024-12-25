using System.ComponentModel.DataAnnotations;

namespace Bag_E_Commerce.Models
{
    public class VendorModel
    {
        [Key]
        public int VendorId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string? ContactDetails { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        
    }
}
