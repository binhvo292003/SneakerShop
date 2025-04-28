using Microsoft.AspNetCore.Mvc;
using SneakerShop.Application.Common.Services;
using SneakerShop.SharedViewModel.Requests.Auth;

namespace SneakerShop.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;

        public AuthController(AuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string email, string password, string name)
        {
            var result = await _service.RegisterUser(email, password, name);
            return result ? Ok("User registered successfully.") : BadRequest("Registration failed.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _service.LoginUser(request.Email, request.Password);
            if (user != null)
            {
            var token = _service.GenerateJwtToken(user);
            return Ok(new { access_token = token });
            }
            return Unauthorized("Invalid credentials.");
        }
    }
}