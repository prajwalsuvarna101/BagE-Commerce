using Bag_E_Commerce.Data;
using Bag_E_Commerce.Models;
using Bag_E_Commerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Bag_E_Commerce.Services
{
    public class UserService : IUserService
    {
        private readonly BagDbContext _context;

        public UserService(BagDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<UserModel> CreateUserAsync(UserModel user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUserAsync(int id, string name, string email, string username, string password)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.name = name;
            user.email = email;
            user.username = username;

            if (!string.IsNullOrEmpty(password))
            {
                user.password_hash = HashPassword(password);
            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(e => e.id == id);
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public async Task<bool> UserExistsAsync(string username, string email)
        {
            return await _context.Users.AnyAsync(u => u.username == username || u.email == email);
        }

    }
}
