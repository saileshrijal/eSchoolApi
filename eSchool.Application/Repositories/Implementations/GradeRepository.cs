using eSchool.Domain.Models;
using eSchool.Infrastructure;
using eSchool.Application.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Onion.Application.Repository.Interface;
using eSchool.Application.Dtos;

namespace eSchool.Application.Repositories.Implementations
{
    public class GradeRepository : Repository<Grade>, IGradeRepository
    {
        public GradeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
