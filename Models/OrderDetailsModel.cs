using System.ComponentModel.DataAnnotations;

namespace Bag_E_Commerce.Models
{
    public class OrderDetailsModel
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal PricePerItem { get; set; }
    }
}
