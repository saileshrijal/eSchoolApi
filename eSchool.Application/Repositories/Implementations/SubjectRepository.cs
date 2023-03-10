using eSchool.Domain.Models;
using eSchool.Application.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Onion.Application.Repository.Interface;
using eSchool.Infrastructure;
using eSchool.Application.Dtos;

namespace eSchool.Application.Repositories.Implementations
{
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
