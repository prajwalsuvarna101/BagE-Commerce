using Bag_E_Commerce.Models;
using System.Threading.Tasks;

namespace Bag_E_Commerce.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> Authenticate(string username, string password);
        Task<UserModel> RegisterAsync(string name, string email, string username, string password, int role);
        bool IsEmailTaken(string email);
        bool IsUsernameTaken(string username);
        string HashPassword(string password);
        
    }
}
