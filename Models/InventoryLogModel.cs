using System.ComponentModel.DataAnnotations;

namespace Bag_E_Commerce.Models
{
    public class InventoryLogModel
    {
        [Key]
        public int LogId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int ChangeQuantity { get; set; }

        [Required]
        public string Reason { get; set; }

        public DateTime LogDate { get; set; } = DateTime.UtcNow;
    }
}
