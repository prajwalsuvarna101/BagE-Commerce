using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Bag_E_Commerce.Models
{
    public class OrderModel
    {
        // Composite primary key with UserId and OrderId
        [Required]
        public int UserId { get; set; }  // Foreign key to UserModel

        [Required]
        public int OrderId { get; set; }  // Unique order identifier for each user

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public string OrderStatus { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property to UserModel
        public UserModel? User { get; set; }  // Optional: allows you to access the full UserModel object

        // Configure composite primary key for UserId and OrderId
        public static void ConfigurePrimaryKey(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderModel>()
                .HasKey(o => new { o.UserId, o.OrderId });  // Composite key of UserId and OrderId
        }
    }
}
