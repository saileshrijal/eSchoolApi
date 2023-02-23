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


        public async Task AddRange(IEnumerable<T> entities)
        {
            await _entities.AddRangeAsync(entities);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        virtual public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        virtual public async Task<T> GetById(int Id)
        {
            var t = await _entities.FindAsync(Id);
            return t!;
        }

        public async Task Insert(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _entities.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}