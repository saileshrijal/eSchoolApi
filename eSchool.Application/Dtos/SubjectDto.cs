using eSchool.Domain.Models;

namespace eSchool.Application.Dtos
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int GradeId { get; set; }
        public GradeDto? Grade { get; set; }
    }
}
