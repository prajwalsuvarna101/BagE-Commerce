using System.ComponentModel.DataAnnotations;

namespace Bag_E_Commerce.Models
{
    public class ReviewModel
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        public int BagId { get; set; }  // Foreign key to BagModel

        [Required]
        public int UserId { get; set; }  // Foreign key to UserModel

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property to BagModel
        public BagModel? Bag { get; set; }

        // Navigation property to UserModel
        public UserModel? User { get; set; }
    }
}
