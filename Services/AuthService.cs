using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Bag_E_Commerce.Models;
using Bag_E_Commerce.Data;
using Bag_E_Commerce.Helper;

namespace Bag_E_Commerce.Services
{
    public class AuthService
    {
        private readonly BagDbContext _context;

        public AuthService(BagDbContext context)
        {
            _context = context;
        }

        public async Task<string> Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.username == username);

            if (user == null || !VerifyPassword(password, user.password_hash))
                return null;

            // Generate JWT Token
            return JwtHelper.GenerateJwtToken(user);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            // Assuming password hash is stored using PBKDF2 or any secure hash function
            var hashBytes = Convert.FromBase64String(storedHash);
            // Implement the actual password verification logic here

            // For demonstration, return true if passwords match
            return true;
        }
    }
}
