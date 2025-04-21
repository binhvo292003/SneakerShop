using SneakerShop.Infrastructure.Data;

namespace SneakerShop.Infrastructure.Repositories
{
    public class BaseRepository
    {
        protected readonly StoreContext _context;

        public BaseRepository(StoreContext context)
        {
            _context = context;
        }
    }
}