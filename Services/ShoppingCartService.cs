using Bag_E_Commerce.Data;
using Bag_E_Commerce.Models;
using Bag_E_Commerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bag_E_Commerce.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly BagDbContext _context;

        public ShoppingCartService(BagDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShoppingCartModel>> GetUserCartAsync(int userId)
        {
            return await _context.Carts
                .Where(cart => cart.UserId == userId)
                .ToListAsync();
        }

        public async Task<ShoppingCartModel> AddToCartAsync(ShoppingCartModel shoppingCart)
        {
            var existingCartItem = await _context.Carts
                .FirstOrDefaultAsync(cart => cart.CartId == shoppingCart.CartId && cart.ProductId == shoppingCart.ProductId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity = shoppingCart.Quantity;
                existingCartItem.PricePerItem = shoppingCart.PricePerItem;
                _context.Carts.Update(existingCartItem);
            }
            else
            {
                await _context.Carts.AddAsync(shoppingCart);
            }

            await _context.SaveChangesAsync();
            return shoppingCart;
        }

        public async Task<ShoppingCartModel> UpdateCartAsync(ShoppingCartModel shoppingCart)
        {
            var existingCartItem = await _context.Carts
                .FirstOrDefaultAsync(cart => cart.UserId == shoppingCart.UserId && cart.ProductId == shoppingCart.ProductId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity = shoppingCart.Quantity;
                existingCartItem.PricePerItem = shoppingCart.PricePerItem;
                _context.Carts.Update(existingCartItem);
                await _context.SaveChangesAsync();
            }

            return existingCartItem;
        }

        public async Task<bool> RemoveFromCartAsync(int userId, int productId)
        {
            var cartItem = await _context.Carts
                .FirstOrDefaultAsync(cart => cart.UserId == userId && cart.ProductId == productId);

            if (cartItem != null)
            {
                _context.Carts.Remove(cartItem);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<decimal> GetTotalPriceAsync(int userId)
        {
            return await _context.Carts
                .Where(cart => cart.UserId == userId)
                .SumAsync(cart => cart.Quantity * cart.PricePerItem);
        }
    }
}
