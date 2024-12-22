using System.ComponentModel.DataAnnotations;

namespace Bag_E_Commerce.Models
{
    public class PaymentModel
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public int OrderId { get; set; }  // Foreign key to OrderModel

        [Required]
        public string PaymentMethod { get; set; }

        [Required]
        public string PaymentStatus { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        // Navigation property to OrderModel
        public OrderModel? Order { get; set; }  // Optional: allows access to the related OrderModel object
    }
}
