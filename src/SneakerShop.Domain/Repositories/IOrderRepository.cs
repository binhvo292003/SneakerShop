using SneakerShop.Domain.Entities;

namespace SneakerShop.Domain.Repositories
{
    public interface IOrderRepository
    {
        public Task<Order> GetOrderById(long id);
        public Task<List<Order>> GetAllOrdersByUserId(long userId);
        public Task<Order> CreateOrder(Order order);
        public Task<Order> UpdateOrder(Order order);
        public Task<bool> DeleteOrder(long id);
    }
}