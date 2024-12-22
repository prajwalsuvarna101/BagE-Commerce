using System.ComponentModel.DataAnnotations;

namespace Bag_E_Commerce.Models
{
    public class OrderDetailsModel
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int OrderItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal PricePerItem { get; set; }

        // Add the BagId property
        [Required]
        public int BagId { get; set; }  // Foreign key to BagModel

        // Navigation property to OrderModel
        public OrderModel? Order { get; set; }

        // Navigation property to BagModel
        public BagModel? Bag { get; set; }  // Navigation property to access the full BagModel
    }
}
