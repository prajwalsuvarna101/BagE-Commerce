using Bag_E_Commerce.Models;

namespace Bag_E_Commerce.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserModel?> GetUserByIdAsync(int id);
        Task<UserModel> CreateUserAsync(UserModel user);
        Task<UserModel> UpdateUserAsync(int id, UserModel user);
        Task<bool> DeleteUserAsync(int id);
    }
}
