using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bag_E_Commerce.Models;
using Bag_E_Commerce.Services.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bag_E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<UserModel>> GetUser(int id)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (currentUserId != id)
            {
                return Unauthorized();
            }

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserModel>> PostUser([FromForm] string Name, [FromForm] string Email, [FromForm] string Username, [FromForm] string Password, [FromForm] int Role)
        {   
            var newUser = new UserModel
                {
                    name = Name,
                    email = Email,
                    username = Username,
                    password_hash = _authService.HashPassword(Password),
                    role = (Enums.UserRole)Role
                };
            var user = await _userService.CreateUserAsync(newUser);
            return CreatedAtAction(nameof(GetUser), new { id = user.id }, user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutUser(int id, [FromForm] string name, [FromForm] string email, [FromForm] string username, [FromForm] string password)
        {
            var updated = await _userService.UpdateUserAsync(id, name, email, username, password);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
