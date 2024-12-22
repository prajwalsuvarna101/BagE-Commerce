using Bag_E_Commerce.Models;

namespace Bag_E_Commerce.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentModel>> GetAllPaymentsAsync();
        Task<PaymentModel?> GetPaymentByIdAsync(int id);
        Task<PaymentModel> CreatePaymentAsync(PaymentModel payment);
        Task<PaymentModel> UpdatePaymentAsync(int id, PaymentModel payment);
        Task<bool> DeletePaymentAsync(int id);
    }
}
