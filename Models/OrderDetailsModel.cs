using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bag_E_Commerce.Models
{
    public class OrderDetailsModel
    {
        [Key]
        public int OrderItemId { get; set; } // Unique per item

        public int OrderId { get; set; } // Foreign key to OrderModel

        public int ProductId { get; set; } // Foreign key to BagModel

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
