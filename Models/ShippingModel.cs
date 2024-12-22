using System.ComponentModel.DataAnnotations;

namespace Bag_E_Commerce.Models
{
    public class ShippingModel
    {
        [Key]
        public int ShippingId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public string ShippingAddress { get; set; }

        [Required]
        public string ShippingStatus { get; set; } = "Pending";

        public DateTime? ShippingDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}
