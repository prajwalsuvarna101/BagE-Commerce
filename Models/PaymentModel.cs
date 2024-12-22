using System.ComponentModel.DataAnnotations;

namespace Bag_E_Commerce.Models
{
    public class PaymentModel
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        [Required]
        public string PaymentStatus { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    }
}
