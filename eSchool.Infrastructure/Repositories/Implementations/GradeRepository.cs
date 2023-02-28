using eSchool.Domain.Models;
using eSchool.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Onion.Infrastructures.Repository.Interface;

namespace eSchool.Infrastructure.Repositories.Implementations
{
    public class GradeRepository : Repository<Grade>, IGradeRepository
    {
        public GradeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<List<Grade>> GetAllAsync()
        {
            return await _context.Grades!.Include(x => x.GradeSubjects)!.ThenInclude(x => x.Subject).ToListAsync();
        }

        public override async Task<Grade> GetByIdAsync(int id)
        {
            var grade = await _context.Grades!.Include(x => x.GradeSubjects)!.ThenInclude(x => x.Subject).FirstOrDefaultAsync(x=>x.Id==id);
            return grade!;
        }
    }
}
