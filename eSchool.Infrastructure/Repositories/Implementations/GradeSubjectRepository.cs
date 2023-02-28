using eSchool.Domain.Models;
using eSchool.Infrastructure.Repositories.Interfaces;
using Onion.Infrastructures.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eSchool.Infrastructure.Repositories.Implementations
{
    public class GradeSubjectRepository : Repository<GradeSubject>, IGradeSubjectRepository
    {
        public GradeSubjectRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
