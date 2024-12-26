using Bag_E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bag_E_Commerce.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderModel>> GetAllOrdersAsync();
        Task<OrderModel> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<OrderModel>> GetOrdersByUserIdAsync(int userId);
        Task<OrderModel> CreateOrderAsync(OrderModel order);
        Task<OrderModel> UpdateOrderAsync(OrderModel order);
        Task<bool> DeleteOrderAsync(int orderId);

        Task<int> CheckoutAsync(int cartId);
        Task<PaymentModel> CompletePayment(int orderId, PaymentMethod paymentMethod);
    }
}
