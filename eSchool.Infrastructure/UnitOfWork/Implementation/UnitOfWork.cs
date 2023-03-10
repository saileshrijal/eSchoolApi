using eSchool.Infrastructure.UnitOfWork.Interface;

namespace eSchool.Infrastructure.UnitOfWork.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync<T>(T entity)
        {
            await _context.AddAsync(entity!);
        }

        public async Task CreateRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            await _context.AddRangeAsync(entities);
        }

        public void Remove<T>(T entity)
        {
            _context.Remove(entity!);
        }

        public void RemoveRange<T>(IEnumerable<T> entities) where T : class
        {
            _context.RemoveRange(entities);
        }

        public void Update<T>(T entity)
        {
            _context.Update(entity!);
        }

        public void UpdateRange<T>(IEnumerable<T> entities) where T : class
        {
            _context.UpdateRange(entities);
        }
    }
}
