using eSchool.Application.Dtos;
using eSchool.Application.Repositories.Interfaces;
using eSchool.Domain.Models;
using eSchool.Infrastructure.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using Onion.Application.Services.Interface;

namespace eSchool.Application.Services.Implementations
{
    public class GradeService : IGradeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGradeRepository _gradeRepository;

        public GradeService(IUnitOfWork unitOfWork, IGradeRepository gradeRepository)
        {
            _unitOfWork = unitOfWork;
            _gradeRepository = gradeRepository;
        }

        public async Task AddSubjectsToGrade(int gradeId, List<int> subjectIds)
        {
            var grade = await _gradeRepository.GetById(gradeId);
            var subjects = await _gradeRepository.GetAllBy(x => subjectIds.Contains(x.Id));
            if (grade == null)
            {
                throw new Exception("Grade doesnote found");
            }
            foreach (var subject in subjects)
            {
                var gradeSubject = new GradeSubject()
                {
                    GradeId = gradeId,
                    SubjectId = subject.Id,
                };
                grade.GradeSubjects?.Add(gradeSubject);
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task CreateGradeAsync(GradeDto gradeDto)
        {
            var checkGradeByNameSection = await _gradeRepository.GetBy(x => x.Name!.ToLower().Trim() == gradeDto.Name!.ToLower().Trim()
                                                    && x.Section!.ToLower().Trim() == gradeDto.Section!.ToLower().Trim());
            if (checkGradeByNameSection != null)
            {
                throw new Exception($"Grade: {gradeDto.Name} {gradeDto.Section} already exists");
            }

            var grade = new Grade()
            {
                Name = gradeDto.Name,
                Section= gradeDto.Section,
            };
            await _unitOfWork.CreateAsync(grade);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteGradeAsync(int id)
        {
            var grade = await _gradeRepository.GetById(id);
            if (grade == null) { throw new Exception("Grade does not found"); }
            _unitOfWork.Remove(grade);
            _unitOfWork.Save();
        }

        public async Task<List<SubjectDto>> GetSubjectsByGradeId(int id)
        {
            var grade = await _gradeRepository.GetById(id);
            var subjectDtos = grade.GradeSubjects!.Select(x => new SubjectDto()
            {
                Id = x.Subject!.Id,
                Name = x.Subject.Name
            }).ToList();
            return subjectDtos.Select(x => new SubjectDto()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }

        public async Task UpdateGradeAsync(int id, GradeDto gradeDto)
        {
            var checkGradeByNameSection = await _gradeRepository.GetBy(x => x.Name!.ToLower().Trim() == gradeDto.Name!.ToLower().Trim()
                                                   && x.Section!.ToLower().Trim() == gradeDto.Section!.ToLower().Trim());
            
            if (checkGradeByNameSection != null)
            {
                if (checkGradeByNameSection.Id != id)
                {
                    throw new Exception($"Grade: {gradeDto.Name} {gradeDto.Section} already exists");
                }
            }

            var grade = await _gradeRepository.GetById(id);
            if (grade == null) { throw new Exception("Grade does not found"); }
            grade.Name = gradeDto.Name;
            grade.Section = gradeDto.Section;
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateGradeSubjects(int gradeId, List<int> subjectIds)
        {
            var grade = await _gradeRepository.GetById(gradeId);

            if (grade == null)
            {
                throw new Exception("Grade doesnote found");
            }

            var existingGradeSubjects = grade.GradeSubjects?.ToList();

            var existingSubjectIds = existingGradeSubjects?.Select(x => x.SubjectId).ToList()!;

            //get the subjects to add or remove
            // 
            var subjectsToAdd = await _gradeRepository.GetAllBy(s => subjectIds.Contains(s.Id) && !existingSubjectIds.Contains(s.Id));
            var subjectsToRemove = existingGradeSubjects?.Where(gs => !subjectIds.Contains(gs.SubjectId)).ToList();

            // Remove existing grade subjects that are no longer associated with the grade
            foreach (var gradeSubject in subjectsToRemove!)
            {
                grade.GradeSubjects?.Remove(gradeSubject);
            }

            // Add new grade subjects that are associated with the grade
            foreach (var subject in subjectsToAdd)
            {
                var gradeSubject = new GradeSubject { GradeId = gradeId, SubjectId = subject.Id };
                grade.GradeSubjects?.Add(gradeSubject);
            }

            await _unitOfWork.SaveAsync();
        }
    }
}
