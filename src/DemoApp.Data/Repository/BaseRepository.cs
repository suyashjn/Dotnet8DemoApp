
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _dbContext;

        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            return await _dbContext.FindAsync<TEntity>(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public void Add(TEntity entity)
        {
            _dbContext.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> items)
        {
            _dbContext.AddRange(items);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Remove(entity);
        }
    }
}
