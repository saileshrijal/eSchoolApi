using eSchool.Application.Dtos;

namespace Onion.Application.Services.Interface
{
    public interface IGradeService
    {
        Task<List<GradeDto>> GetAllGradesAsync();
        Task<GradeDto> GetGradeByIdAsync(int id);
        Task CreateGradeAsync(GradeDto gradeDto);
        Task UpdateGradeAsync(int id, GradeDto gradeDto);
        Task DeleteGradeAsync(int id);
    }
}