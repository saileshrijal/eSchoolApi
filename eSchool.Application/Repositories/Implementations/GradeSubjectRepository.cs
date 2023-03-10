using eSchool.Domain.Models;
using eSchool.Application.Repositories.Interfaces;
using Onion.Application.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using eSchool.Infrastructure;
using eSchool.Application.Dtos;
using Microsoft.EntityFrameworkCore;

namespace eSchool.Application.Repositories.Implementations
{
    public class GradeSubjectRepository : Repository<GradeSubject>, IGradeSubjectRepository
    {
        public GradeSubjectRepository(ApplicationDbContext context) : base(context)
        {
           
        }

        public async Task<List<GradeSubjectDto>> GetGradesWithSubjects()
        {
            return  await _context.GradeSubjects!.Select(x=>new GradeSubjectDto()
            {
                Grade = new GradeDto()
                {
                    Id = x.Grade!.Id,
                    Name = x.Grade.Name,
                    Section = x.Grade.Section,
                },
                Subject = new SubjectDto()
                {
                    Id = x.Subject!.Id,
                    Name = x.Subject.Name
                }
            }).ToListAsync();
        }
    }
}
