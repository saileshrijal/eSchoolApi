using eSchool.Domain.Models;
using eSchool.Infrastructure.Repositories.Interfaces;

namespace eSchool.Infrastructure.Repositories.Implementations
{
    public class GradeRepository:GenericRepository<Grade>,IGradeRepository
    {
        public GradeRepository(ApplicationDbContext context):base(context)
        {
            
        }
    }
}
