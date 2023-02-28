using System.ComponentModel.DataAnnotations;

namespace eSchool.Presentation.ViewModels
{
    public class SubjectVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? GradeId { get; set; }
    }
}
