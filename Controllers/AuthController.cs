using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Bag_E_Commerce.Services.Interfaces;
using Bag_E_Commerce.Models;
using Bag_E_Commerce.DTO; 
using System;

namespace Bag_E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SignupRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { Message = "All fields are required." });
            }

            try
            {
                // Check if username or email already exists
                if (await _userService.UserExistsAsync(request.Username, request.Email))
                {
                    return BadRequest(new { Message = "Username or email already exists." });
                }

                // Create the user
                var newUser = new UserModel
                {
                    name = request.Name,
                    email = request.Email,
                    username = request.Username,
                    password_hash = _authService.HashPassword(request.Password),
                    role = (Enums.UserRole)request.Role
                };

                var createdUser = await _userService.CreateUserAsync(newUser);

                return Ok(new { Message = "User registered successfully.", User = createdUser });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class SignupRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }
}
