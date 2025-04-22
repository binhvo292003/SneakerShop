using SneakerShop.Domain.Repositories;

namespace SneakerShop.Application.Common.Services
{
    public class CartService
    {
        public ICartItemRepository CartItemRepository { get; }
        public CartService(ICartItemRepository cartItemRepository)
        {
            CartItemRepository = cartItemRepository;
        }

    }
}