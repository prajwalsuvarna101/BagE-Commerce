using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Bag_E_Commerce.Data;
using Bag_E_Commerce.Helper;
using Bag_E_Commerce.Models;
using Bag_E_Commerce.Services.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;


namespace Bag_E_Commerce.Services
{
    public class AuthService : IAuthService
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

        public async Task<UserModel> RegisterAsync(string name, string email, string username, string password, int role)
        {
            // Check if email or username is already taken
            if (IsEmailTaken(email))
                throw new ArgumentException("Email is already registered.");

            if (IsUsernameTaken(username))
                throw new ArgumentException("Username is already taken.");

            // Hash the password
            string hashedPassword = HashPassword(password);

            // Create the user model
            var user = new UserModel
            {
                name = name,
                email = email,
                username = username,
                password_hash = hashedPassword,
                role = (Enums.UserRole)role,
                created_at = DateTime.UtcNow
            };

            // Save to database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public bool IsEmailTaken(string email)
        {
            return _context.Users.Any(u => u.email == email);
        }

        public bool IsUsernameTaken(string username)
        {
            return _context.Users.Any(u => u.username == username);
        }

        public string HashPassword(string password)
        {
            // Generate a salt
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Derive the hash
            byte[] hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32);

            // Combine the salt and hash
            byte[] hashBytes = new byte[48];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 32);

            // Convert to Base64
            return Convert.ToBase64String(hashBytes);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            // Decode the stored hash
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            // Extract the salt
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Hash the input password using the same salt
            byte[] hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32);

            // Compare the results
            for (int i = 0; i < 32; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }

            return true;
        }
    }
}
