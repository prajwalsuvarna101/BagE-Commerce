using System.Collections.Generic;
using System.Threading.Tasks;
using Bag_E_Commerce.Models;

namespace Bag_E_Commerce.Services
{
    public interface IOrderDetailsService
    {
        Task<IEnumerable<OrderDetailsModel>> GetAllOrderDetailsAsync();
        Task<IEnumerable<OrderDetailsModel>> GetOrderDetailsByOrderIdAsync(int orderId);
        Task<int> DeleteOrderDetailsByOrderIdAsync(int orderId);
    }
}
