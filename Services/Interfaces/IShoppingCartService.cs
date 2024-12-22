using Bag_E_Commerce.Models;

namespace Bag_E_Commerce.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<ShoppingCartModel>> GetAllCartItemsAsync(int userId);
        Task<ShoppingCartModel?> GetCartItemByIdAsync(int userId, int productId);
        Task<ShoppingCartModel> AddToCartAsync(ShoppingCartModel cartItem);
        Task<ShoppingCartModel> UpdateCartItemAsync(int userId, int productId, ShoppingCartModel cartItem);
        Task<bool> RemoveFromCartAsync(int userId, int productId);
    }
}