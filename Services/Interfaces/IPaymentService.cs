using System.Collections.Generic;
using System.Threading.Tasks;
using Bag_E_Commerce.Models;

namespace Bag_E_Commerce.Services
{
    public interface IPaymentService
    {
        Task<List<PaymentModel>> GetAllPaymentsAsync();
        Task<List<PaymentModel>> GetPaymentsByOrderIdAsync(int orderId);
    }
}
