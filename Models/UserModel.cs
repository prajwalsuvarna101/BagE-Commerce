using System.ComponentModel.DataAnnotations;
using Bag_E_Commerce.Enums;

namespace Bag_E_Commerce.Models
{
    public class UserModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string name { get; set; }

        [Required]
        [EmailAddress]
        public required string email { get; set; }

        [Required]
        [MaxLength(50)]
        public required string username { get; set; }

        [Required]
        [MaxLength(100)]
        public string? password_hash { get; set; }

        public UserRole role { get; set; } = UserRole.User; // Default role is User

        public DateTime created_at { get; set; } = DateTime.UtcNow;
    }
}
