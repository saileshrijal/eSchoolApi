using eSchool.Application.Services.Interfaces;
using eSchool.Domain.Models;
using eSchool.Infrastructure.UnitOfWork.Interface;

namespace eSchool.Application.Services.Implementations
{
    public class SubjectService:ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Create(Subject subject)
        {
            var checkSubjectByName = await _unitOfWork.Subject.GetBy(x => x.Name!.ToLower().Trim() == subject.Name!.ToLower().Trim());
            if (checkSubjectByName != null)
            {
                throw new DuplicateWaitObjectException($"Subject: {subject.Name} already exists");
            }

            await _unitOfWork.Subject.Create(subject);
            return await _unitOfWork.SaveAsync();

        }

        public async Task<int> Delete(int id)
        {
            var checkSubject = await _unitOfWork.Subject.GetBy(x => x.Id == id);
            if (checkSubject == null)
            {
                throw new Exception($"Subject with ID: {id} does not exists");
            }
            await _unitOfWork.Subject.Delete(id);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> Edit(Subject subject)
        {
            var checkSubjectByName = await _unitOfWork.Subject.GetBy(x => x.Name!.ToLower().Trim() == subject.Name!.ToLower().Trim());

            if (checkSubjectByName != null)
            {
                if (checkSubjectByName.Id != subject.Id)
                {
                    throw new DuplicateWaitObjectException($"Subject: {subject.Name} already exists");
                }
            }
            _unitOfWork.Subject.Edit(subject);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<Subject> Get(int id)
        {
            var subject = await _unitOfWork.Subject.GetBy(x => x.Id == id);
            if (subject == null)
            {
                throw new Exception($"Subject with ID: {id} does not exists");
            }
            return subject;
        }

        public async Task<List<Subject>> GetAll()
        {
            return await _unitOfWork.Subject.GetAll();
        }
    }
}
