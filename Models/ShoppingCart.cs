using System.ComponentModel.DataAnnotations;

namespace Bag_E_Commerce.Models
{
    public class ShoppingCartModel
    {
        public int CartId { get; set; }  // Primary key for the shopping cart item
        public int ProductId { get; set; }  // Part of composite key
        public int UserId { get; set; }  // Foreign Key for User
        public decimal PricePerItem { get; set; }
        public int Quantity { get; set; }
    }
}
