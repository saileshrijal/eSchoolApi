using System.Linq.Expressions;

namespace eSchool.Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetAllBy(Expression<Func<T, bool>> predicate);
        Task Create(T t);
        public Task Delete(int id);
        void Edit(T t);
        public Task<T?> GetBy(Expression<Func<T, bool>> predicate);
    }
}
