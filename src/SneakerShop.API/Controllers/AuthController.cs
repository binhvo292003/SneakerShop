using Microsoft.AspNetCore.Mvc;
using SneakerShop.Application.Common.Services;
using SneakerShop.SharedViewModel.Requests.Auth;
using SneakerShop.SharedViewModel.Responses.Auth;

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
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var email = request.Email;
            var password = request.Password;
            var name = request.Name;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name))
            {
                return BadRequest("Email, password, and name are required.");
            }

            var checkEmail = await _service.CheckEmailExists(email);
            if (checkEmail)
            {
                return BadRequest("Email already exists.");
            }

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
                var response = new LoginResponse
                {
                    Token = token,
                    Email = user.Email,
                    UserId = user.Id.ToString()
                };
                return Ok(response);
            }
            return Unauthorized("Invalid credentials.");
        }
    }
}