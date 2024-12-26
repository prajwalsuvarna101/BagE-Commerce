using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bag_E_Commerce.Models
{
    public class ShoppingCartModel
    {
        [Key]
        public int CartId { get; set; }

        [Key]
        public int ProductId { get; set; }

        public int UserId { get; set; }  // Foreign Key for User
        public decimal PricePerItem { get; set; }
        public int Quantity { get; set; }
    }
}
