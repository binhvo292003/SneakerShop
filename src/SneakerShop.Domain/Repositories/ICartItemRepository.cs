using SneakerShop.Domain.Entities;

namespace SneakerShop.Domain.Repositories
{
    public interface ICartItemRepository
    {
        public Task<CartItem> GetCartItemById(long id);
        public Task<List<CartItem>> GetAllCartItemsByUserId(long userId);
        public Task<CartItem> CreateCartItem(CartItem cartItem);
        public Task<CartItem> UpdateCartItem(CartItem cartItem);
        // soft delete cart item
        public Task<bool> DeleteCartItem(long id);
    }
}