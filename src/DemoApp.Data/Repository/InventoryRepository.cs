using DemoApp.Data.Entities;

namespace DemoApp.Data.Repository
{
    public class InventoryRepository : BaseRepository<InventoryItem>, IInventoryRepository
    {
        public InventoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
