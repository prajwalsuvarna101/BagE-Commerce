using System.ComponentModel.DataAnnotations;

namespace Bag_E_Commerce.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }

        // Navigation property for related bags
       
    }
}
