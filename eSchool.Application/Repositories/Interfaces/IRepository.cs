using System.Linq.Expressions;

namespace Onion.Application.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllBy(Expression<Func<T, bool>> predicate);
        Task<T> GetBy(Expression<Func<T, bool>> predicate);
        Task<T> GetById(int Id);
    }
}