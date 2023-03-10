using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSchool.Application.Dtos
{
    public class GradeSubjectDto
    {
        public GradeDto? Grade { get; set; }
        public SubjectDto? Subject { get; set; }
    }
}
