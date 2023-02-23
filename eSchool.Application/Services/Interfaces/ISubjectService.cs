
using eSchool.Domain.Models;

namespace eSchool.Application.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<List<Subject>> GetAll();
        Task<Subject> Get(int id);
        Task<int> Delete(int id);
        Task<int> Edit(Subject subject);
        Task<int> Create(Subject subject);
    }
}
