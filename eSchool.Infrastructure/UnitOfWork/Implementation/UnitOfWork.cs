using eSchool.Infrastructure.Repositories.Implementations;
using eSchool.Infrastructure.Repositories.Interfaces;
using eSchool.Infrastructure.UnitOfWork.Interface;

namespace eSchool.Infrastructure.UnitOfWork.Implementation
{
    public class UnitOfWork:IUnitOfWork
    {
        private ApplicationDbContext _context;
        public IGradeRepository Grade { get; private set; }
        public ISubjectRepository Subject { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {

            _context = context;
            Grade = new GradeRepository(context);
            Subject = new SubjectRepository(context);
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
