using ERP_LicoExpress_API.Models;
using ERP_LicoExpress_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_LicoExpress_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserService _userService;
        public AuthController(UserService userService)
        {
            this._userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User model)
        {
            var user = await _userService.Authenticate(model);

            if (user == null)
            {
                return Unauthorized();
            }

            var token = _userService.GenerateToken(user);

            return Ok(new { token });
        }
    }
}
