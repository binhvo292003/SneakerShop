using SneakerShop.Domain.Entities;

namespace SneakerShop.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetUserById(long id);
        public Task<User> GetUserByEmail(string email);
        public Task<User> GetUserByUsername(string username);
        public Task<List<User>> GetAllUsers();
        public Task<User> CreateUser(User user);
        public Task<User> UpdateUser(User user);
        // soft delete user
        public Task<bool> DeleteUser(long id);
    }
}