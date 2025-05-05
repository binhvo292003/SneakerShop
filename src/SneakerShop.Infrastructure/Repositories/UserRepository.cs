using Microsoft.EntityFrameworkCore;
using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories;
using SneakerShop.Infrastructure.Data;

namespace SneakerShop.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(StoreContext context) : base(context)
        {
        }

        public Task<User> AddCategoryToUser(long userId, long categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<User> AddProductToUser(long userId, long productId)
        {
            throw new NotImplementedException();
        }
        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            System.Console.WriteLine("User created: " + user.Id);
            return user;
        }

        public Task<bool> DeleteUser(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserById(long id)
        {
            return await _context.Users.FindAsync(id);
        }

        public Task<User> GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}