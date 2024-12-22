using Bag_E_Commerce.Models;

namespace Bag_E_Commerce.Services.Interfaces
{
    public interface IOrderDetailsService
    {
        Task<IEnumerable<OrderDetailsModel>> GetAllOrderDetailsAsync();
        Task<OrderDetailsModel?> GetOrderDetailByIdAsync(int orderId, int productId);
        Task<OrderDetailsModel> CreateOrderDetailAsync(OrderDetailsModel orderDetail);
        Task<OrderDetailsModel> UpdateOrderDetailAsync(int orderId, int productId, OrderDetailsModel orderDetail);
        Task<bool> DeleteOrderDetailAsync(int orderId, int productId);
    }
}
