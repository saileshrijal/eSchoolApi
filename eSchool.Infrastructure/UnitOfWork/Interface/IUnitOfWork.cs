using eSchool.Infrastructure.Repositories.Interfaces;

namespace eSchool.Infrastructure.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {
        IGradeRepository Grade { get; }
        IGradeSubjectRepository GradeSubject { get; }
        ISubjectRepository  Subject { get; }
        Task<int> SaveAsync();
    }
}
