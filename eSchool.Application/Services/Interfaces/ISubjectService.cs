using eSchool.Application.Dtos;

namespace eSchool.Application.Services.Interfaces
{
    public interface ISubjectService
    {
        Task CreateSubjectAsync(SubjectDto subjectDto);
        Task UpdateSubjectAsync(int id, SubjectDto subjectDto);
        Task DeleteSubjectAsync(int id);
    }
}
