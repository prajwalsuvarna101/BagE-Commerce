using Bag_E_Commerce.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bag_E_Commerce.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByIdAsync(int id);
        Task<UserModel> CreateUserAsync(UserModel user);
        Task<bool> UpdateUserAsync(int id, string name, string email, string username, string password);
        Task<bool> DeleteUserAsync(int id);
        bool UserExists(int id);
        string HashPassword(string password);
        Task<bool> UserExistsAsync(string username, string email);

    }
}
