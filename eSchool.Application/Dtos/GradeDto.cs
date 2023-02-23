namespace eSchool.Application.Dtos
{
    public class GradeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Section { get; set; }
        public List<SubjectDto>? Subjects { get; set; }
    }
}
