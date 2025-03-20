namespace DemoApp.Data.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        public Task<TEntity> GetAsync(Guid id);
        public Task<IEnumerable<TEntity>> GetAllAsync();
        public void Add(TEntity enity);
        public void AddRange(IEnumerable<TEntity> entities);
        public void Delete(TEntity entity);
    }
}
