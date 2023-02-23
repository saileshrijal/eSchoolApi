using eSchool.Domain.Models;
using eSchool.Infrastructure.Repositories.Interfaces;
using Onion.Infrastructures.Repository.Interface;

namespace eSchool.Infrastructure.Repositories.Implementations
{
    public class GradeRepository : Repository<Grade>, IGradeRepository
    {
        public GradeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
