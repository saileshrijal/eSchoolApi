using eSchool.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Onion.Application.Repository.Interface
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllBy(Expression<Func<T, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        public async Task<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            var t = await _entities.FirstOrDefaultAsync(predicate)!;
            return t!;
        }

        public async Task<T> GetById(int Id)
        {
            var t = await _entities.FindAsync(Id);
            return t!;
        }
    }
}