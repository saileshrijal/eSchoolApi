using eSchool.Application.Dtos;
using eSchool.Application.Services.Interfaces;
using eSchool.Domain.Models;
using eSchool.Infrastructure.UnitOfWork.Interface;

namespace eSchool.Application.Services.Implementations
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateSubjectAsync(SubjectDto subjectDto)
        {
            var subject = new Subject()
            {
                Name = subjectDto.Name,
                GradeId = subjectDto.GradeId,

            };
            await _unitOfWork.Subject.AddAsync(subject);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteSubjectAsync(int id)
        {
            var subject = await _unitOfWork.Subject.GetByIdAsync(id);
            if (subject == null) { throw new Exception("Subject not found"); }
            _unitOfWork.Subject.Remove(subject);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<SubjectDto>> GetAllSubjectAsync()
        {
            var listOfSubject = await _unitOfWork.Subject.GetAllAsync();
            return listOfSubject.Select(x => new SubjectDto()
            {
                Id = x.Id,
                Name = x.Name,
                GradeId = x.GradeId,    
            }).ToList();
        }

        public async Task<SubjectDto> GetSubjectByIdAsync(int id)
        {
            var subject = await _unitOfWork.Subject.GetByIdAsync(id);
            return new SubjectDto()
            {
                Id = subject.Id,
                Name = subject.Name,
                GradeId = subject.GradeId,
            };
        }

        public async Task UpdateSubjectAsync(int id, SubjectDto subjectDto)
        {
            var subject = await _unitOfWork.Subject.GetByIdAsync(id);
            if (subject == null) { throw new Exception("Subject not found"); }
            subject.Name = subjectDto.Name;
            await _unitOfWork.SaveAsync();
        }
    }
}
