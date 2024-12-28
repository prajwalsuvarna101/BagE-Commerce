using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Bag_E_Commerce.Services;
using Microsoft.AspNetCore.Authorization;

namespace Bag_E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { Message = "Username and password must be provided." });
            }

            try
            {
                var token = await _authService.Authenticate(request.Username, request.Password);

                if (token == null)
                    return Unauthorized(new { Message = "Invalid username or password." });

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

    }
}
