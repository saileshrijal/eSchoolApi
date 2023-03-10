using eSchool.Application.Dtos;

namespace Onion.Application.Services.Interface
{
    public interface IGradeService
    {
        Task CreateGradeAsync(GradeDto gradeDto);
        Task UpdateGradeAsync(int id, GradeDto gradeDto);
        Task DeleteGradeAsync(int id);
        Task AddSubjectsToGrade(int gradeId, List<int> subjectIds);
        Task UpdateGradeSubjects(int gradeId, List<int> subjectIds);
    }
}