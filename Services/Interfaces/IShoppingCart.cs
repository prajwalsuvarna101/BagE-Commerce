using Bag_E_Commerce.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bag_E_Commerce.Services
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<ShoppingCartModel>> GetUserCartAsync(int userId);
        Task<ShoppingCartModel> AddToCartAsync(ShoppingCartModel shoppingCart);
        Task<ShoppingCartModel> UpdateCartAsync(ShoppingCartModel shoppingCart);
        Task<bool> RemoveFromCartAsync(int userId, int productId);
        Task<bool> RemoveItemsFromCartAsync(int cartId);
        Task<decimal> GetTotalPriceAsync(int userId);
    }
}
