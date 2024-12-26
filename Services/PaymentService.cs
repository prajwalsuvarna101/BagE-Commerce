using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bag_E_Commerce.Data;
using Bag_E_Commerce.Models;

namespace Bag_E_Commerce.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly BagDbContext _context;

        public PaymentService(BagDbContext context)
        {
            _context = context;
        }

        public async Task<List<PaymentModel>> GetAllPaymentsAsync()
        {
            // Fetch all payments from the database
            return await Task.FromResult(_context.Payment.ToList());
        }

        public async Task<List<PaymentModel>> GetPaymentsByOrderIdAsync(int orderId)
        {
            // Fetch payments filtered by OrderId
            return await Task.FromResult(_context.Payment.Where(p => p.OrderId == orderId).ToList());
        }
    }
}
