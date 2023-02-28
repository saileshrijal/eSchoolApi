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
    }
}
