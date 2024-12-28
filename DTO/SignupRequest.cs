using Bag_E_Commerce.Enums;

namespace Bag_E_Commerce.DTO
{
    public class SignupRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole? Role { get; set; } // Optional role field
    }
}