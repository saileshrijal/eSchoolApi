using eSchool.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Infrastructures.Repository.Interface
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


        virtual public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _entities.AddRangeAsync(entities);
            _context.SaveChanges();
        }

        virtual public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        virtual public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        virtual public async Task<T> GetByIdAsync(int id)
        {
            var t = await _entities.FindAsync(id);
            return t!;
        }

        virtual public async Task AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
        }

        virtual public void Remove(T entity)
        {
            _entities.Remove(entity);
        }

        virtual public void RemoveRange(IEnumerable<T> entities)
        {
            _entities.RemoveRange(entities);
        }

        virtual public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        virtual public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            var grade = await _entities.FirstOrDefaultAsync(predicate);
            return grade!;
        }
    }
}