namespace eSchool.Infrastructure.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
    }
}
