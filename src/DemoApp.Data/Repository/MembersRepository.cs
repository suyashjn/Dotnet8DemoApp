using DemoApp.Data.Entities;

namespace DemoApp.Data.Repository
{
    public class MembersRepository : BaseRepository<Member>, IMembersRepository
    {
        public MembersRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
