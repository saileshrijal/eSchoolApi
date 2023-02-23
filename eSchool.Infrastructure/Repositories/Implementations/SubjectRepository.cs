using eSchool.Domain.Models;
using eSchool.Infrastructure.Repositories.Interfaces;

namespace eSchool.Infrastructure.Repositories.Implementations
{
    public class SubjectRepository: GenericRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext context):base(context)
        {
            
        }
    }
}
