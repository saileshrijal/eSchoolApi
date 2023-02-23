
using eSchool.Domain.Models;

namespace eSchool.Application.Services.Interfaces
{
    public interface IGradeService
    {
        Task<List<Grade>> GetAll();
        Task<Grade> Get(int id);
        Task<int> Delete(int id);
        Task<int> Edit(Grade grade);
        Task<int> Create(Grade grade);
    }
}
