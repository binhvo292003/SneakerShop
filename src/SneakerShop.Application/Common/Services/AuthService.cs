using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CloudinaryDotNet.Actions;
using Microsoft.IdentityModel.Tokens;
using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories;

namespace SneakerShop.Application.Common.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterUser(string email, string password, string name)
        {
            var user = new User
            {
                Email = email,
                Password = password,
                Name = name,
            };

            var result = await _userRepository.CreateUser(user);

            if (result == null)
            {
                return false;
            }
            return true;
        }

        public async Task<User> LoginUser(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);

            return user != null && user.Password == password ? user : null;
        }

        public async Task<bool> LogoutUser(long userId)
        {
            // Handle logic
            var user = await _userRepository.GetUserById(userId);

            return true;
        }

        public string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_key_1234567890"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            return user != null;
        }

    }
}