using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bag_E_Commerce.Models
{
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }  // Primary key

        public int UserId { get; set; }  // Foreign key to Users

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public string OrderStatus { get; set; }  // 'Pending', 'Completed', or 'Cancelled'

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Default to current UTC time
    }
}
