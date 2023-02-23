using eSchool.Domain.Models;
using eSchool.Infrastructure.Repositories.Interfaces;
using Onion.Infrastructures.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSchool.Infrastructure.Repositories.Implementations
{
    public class GradeRepository : Repository<Grade>, IGradeRepository
    {
        public GradeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
