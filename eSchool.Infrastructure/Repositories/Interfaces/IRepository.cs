using System.Linq.Expressions;

namespace Onion.Infrastructures.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task AddRangeAsync(List<T> entities);
        Task AddAsync(T entity);
        Task<T> GetByIdAsync(int id);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(List<T> entities);
    }
}