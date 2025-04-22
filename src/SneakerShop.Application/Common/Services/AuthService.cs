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

        public async Task<bool> LoginUser(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);

            if (user == null || user.Password != password)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> LogoutUser(long userId)
        {
            // Handle logic
            var user = await _userRepository.GetUserById(userId);

            return true;
        }

    }
}