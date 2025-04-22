using Microsoft.AspNetCore.Mvc;
using SneakerShop.Application.Common.Services;

namespace SneakerShop.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController:ControllerBase
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
    }
}