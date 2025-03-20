using DemoApp.Data.Entities;

namespace DemoApp.Data.Repository
{
    public class BookingsRepository : BaseRepository<Booking>, IBookingsRepository
    {
        public BookingsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
