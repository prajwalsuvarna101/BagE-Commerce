using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bag_E_Commerce.Models
{
    public class ReviewModel
    {
        [Key]
        public int ReviewId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Range(1, 5)]
        public byte Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
