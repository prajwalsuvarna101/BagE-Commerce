using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Bag_E_Commerce.Services;
using Microsoft.AspNetCore.Authorization;

namespace Bag_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAllPayments()
        {
            try
            {
                // Call the service to get all payments
                var payments = await _paymentService.GetAllPaymentsAsync();
                return Ok(payments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("order/{orderId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetPaymentsByOrderId(int orderId)
        {
            try
            {
                // Call the service to get payments for a specific order
                var payments = await _paymentService.GetPaymentsByOrderIdAsync(orderId);
                return Ok(payments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
