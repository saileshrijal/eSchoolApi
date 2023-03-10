using eSchool.Application.Dtos;
using eSchool.Application.Repositories.Interfaces;
using eSchool.Application.Services.Interfaces;
using eSchool.Domain.Models;
using eSchool.Infrastructure.UnitOfWork.Interface;

namespace eSchool.Application.Services.Implementations
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(IUnitOfWork unitOfWork,
                               ISubjectRepository subjectRepository)
        {
            _unitOfWork = unitOfWork;
            _subjectRepository = subjectRepository;
        }

        public async Task CreateSubjectAsync(SubjectDto subjectDto)
        {
            var checkSubjectByName = await _subjectRepository.GetBy(x => x.Name!.ToLower().Trim() == subjectDto.Name!.ToLower().Trim());
            
            if (checkSubjectByName != null)
            {
                throw new Exception($"Subject: {subjectDto.Name} already exists");
            }

            var subject = new Subject()
            {
                Name = subjectDto.Name,
            };
            await _unitOfWork.CreateAsync(subject);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteSubjectAsync(int id)
        {
            var subject = await _subjectRepository.GetById(id);
            if (subject == null) { throw new Exception("Subject not found"); }
            _unitOfWork.Remove(subject);
            _unitOfWork.Save();
        }

        public async Task UpdateSubjectAsync(int id, SubjectDto subjectDto)
        {
            var checkSubjectByName = await _subjectRepository.GetBy(x => x.Name!.ToLower().Trim() == subjectDto.Name!.ToLower().Trim());

            if (checkSubjectByName != null)
            {
                if (checkSubjectByName.Id != id)
                {
                    throw new Exception($"Subject: {subjectDto.Name} already exists");
                }
            }

            var subject = await _subjectRepository.GetById(id);
            if (subject == null) { throw new Exception("Subject not found"); }
            subject.Name = subjectDto.Name;
            await _unitOfWork.SaveAsync();
        }
    }
}
