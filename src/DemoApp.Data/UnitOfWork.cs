using DemoApp.Data.Repository;

namespace DemoApp.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public IMembersRepository Members { get; }
        public IInventoryRepository Inventory { get; }
        public IBookingsRepository Bookings { get; }

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            Members = new MembersRepository(_dbContext);
            Inventory = new InventoryRepository(_dbContext);
            Bookings = new BookingsRepository(_dbContext);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
