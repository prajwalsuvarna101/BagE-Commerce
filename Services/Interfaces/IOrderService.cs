using Bag_E_Commerce.Models;

namespace Bag_E_Commerce.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderModel>> GetAllOrdersAsync();
        Task<OrderModel?> GetOrderByIdAsync(int id);
        Task<OrderModel> CreateOrderAsync(OrderModel order);
        Task<OrderModel> UpdateOrderAsync(int id, OrderModel order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
