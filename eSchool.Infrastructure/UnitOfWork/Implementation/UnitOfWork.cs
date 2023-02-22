using eSchool.Infrastructure.UnitOfWork.Interface;

namespace eSchool.Infrastructure.UnitOfWork.Implementation
{
    public class UnitOfWork:IUnitOfWork
    {
        private ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {

            _context = context;

        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
