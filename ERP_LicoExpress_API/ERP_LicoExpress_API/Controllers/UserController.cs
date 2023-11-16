using ERP_LicoExpress_API.Models;
using ERP_LicoExpress_API.Services;
using ERP_LicoExpress_API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ERP_LicoExpress_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var usuarios = await _userService
                .GetAllAsync();

            return Ok(usuarios);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                var userIngresado = await _userService.LoginAsync(user);

                return Ok(userIngresado);
            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");

            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> CreateUserAsync(User user)
        {
            try
            {
                var userIngresado = await _userService.CreateUserAsync(user);

                return Ok(userIngresado);
            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");

            }
        }

        [HttpDelete("{user_id int}")]
        public async Task<IActionResult> DeleteAsync(int user_id)
        {
            try
            {
                var userIngresado = await _userService.DeleteAsync(user_id);

                return Ok(userIngresado);
            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");

            }
        }

    }
}
