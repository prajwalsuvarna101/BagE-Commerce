using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bag_E_Commerce.Models
{
    public class ShippingModel
    {
        [Key]
        public int ShippingId { get; set; } // Primary Key
        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; } // Foreign Key referencing Orders table
        [Required]
        public string ShippingAddress { get; set; } = string.Empty;
        [Required]
        public ShippingStatus ShippingStatus { get; set; } = ShippingStatus.Pending;
        public DateTime? ShippingDate { get; set; }
        public DateTime? DeliveryDate { get; set; } 
    }

    public enum ShippingStatus
    {
        Pending,
        Shipped,
        Delivered,
        Cancelled
    }
}
