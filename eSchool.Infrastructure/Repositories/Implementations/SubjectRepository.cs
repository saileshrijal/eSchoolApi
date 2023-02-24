using eSchool.Domain.Models;
using eSchool.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Onion.Infrastructures.Repository.Interface;

namespace eSchool.Infrastructure.Repositories.Implementations
{
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _context.Subjects!.Include(x=>x.Grade).ToListAsync();
        }

        public override async Task<Subject> GetByIdAsync(int id)
        {
            var subject = await _context.Subjects!.Include(x=>x.Grade).FirstOrDefaultAsync(x=>x.Id==id);
            return subject!;
        }
    }
}
