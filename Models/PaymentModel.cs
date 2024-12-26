using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bag_E_Commerce.Models
{
    public class PaymentModel
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public PaymentStatus PaymentStatus { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        // Navigation property (if needed)
        // public virtual OrderModel? Order { get; set; }
    }

    public enum PaymentMethod
    {
        Card,
        Paypal,
        Cash
    }

    public enum PaymentStatus
    {
        Success,
        Failed,
        Pending
    }
}
