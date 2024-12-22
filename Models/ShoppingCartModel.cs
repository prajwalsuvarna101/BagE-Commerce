using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Bag_E_Commerce.Models
{
    public class ShoppingCartModel
    {
        [Required]
        public int UserId { get; set; }  // Foreign key to UserModel

        [Required]
        public int CartId { get; set; }  // Assuming CartId is unique per user

        [Required]
        public int BagId { get; set; }  // Foreign key to BagModel

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal PricePerItem { get; set; }

        // Navigation properties
        public UserModel? User { get; set; }  // Navigation property to UserModel
        public BagModel? Bag { get; set; }    // Navigation property to BagModel

        // Composite primary key
        public static void ConfigurePrimaryKey(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingCartModel>()
                .HasKey(sc => new { sc.UserId, sc.CartId, sc.BagId });  // Composite key of UserId, CartId, and BagId
        }
    }
}
