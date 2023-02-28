using eSchool.Application.Dtos;
using eSchool.Domain.Models;
using eSchool.Infrastructure.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using Onion.Application.Services.Interface;

namespace eSchool.Application.Services.Implementations
{
    public class GradeService : IGradeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GradeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddSubjectsToGrade(int gradeId, List<int> subjectIds)
        {
            var grade = await _unitOfWork.Grade.GetByIdAsync(gradeId);
            var subjects = await _unitOfWork.Subject.FindAsync(x => subjectIds.Contains(x.Id));
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
            var checkGradeByNameSection = await _unitOfWork.Grade.GetAsync(x => x.Name!.ToLower().Trim() == gradeDto.Name!.ToLower().Trim()
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
            await _unitOfWork.Grade.AddAsync(grade);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteGradeAsync(int id)
        {
            var grade = await _unitOfWork.Grade.GetByIdAsync(id);
            if (grade == null) { throw new Exception("Grade does not found"); }
            _unitOfWork.Grade.Remove(grade);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<GradeDto>> GetAllGradesAsync()
        {
            var listOfGrade = await _unitOfWork.Grade.GetAllAsync();
            return listOfGrade.Select(x => new GradeDto()
            {
                Id = x.Id,
                Name = x.Name,
                Section = x.Section,
            }).ToList();
        }

        public async Task<GradeDto> GetGradeByIdAsync(int id)
        {
            var grade = await _unitOfWork.Grade.GetByIdAsync(id);
            return new GradeDto()
            {
                Id = grade.Id,
                Name = grade.Name,
                Section = grade.Section,
            };
        }

        public async Task<List<SubjectDto>> GetSubjectsByGradeId(int id)
        {
            var grade = await _unitOfWork.Grade.GetByIdAsync(id);
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
            var checkGradeByNameSection = await _unitOfWork.Grade.GetAsync(x => x.Name!.ToLower().Trim() == gradeDto.Name!.ToLower().Trim()
                                                   && x.Section!.ToLower().Trim() == gradeDto.Section!.ToLower().Trim());

            if (checkGradeByNameSection != null)
            {
                if (checkGradeByNameSection.Id != id)
                {
                    throw new Exception($"Grade: {gradeDto.Name} {gradeDto.Section} already exists");
                }
            }

            var grade = await _unitOfWork.Grade.GetByIdAsync(id);
            if (grade == null) { throw new Exception("Grade does not found"); }
            grade.Name = gradeDto.Name;
            grade.Section = gradeDto.Section;
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateGradeSubjects(int gradeId, List<int> subjectIds)
        {
            var grade = await _unitOfWork.Grade.GetByIdAsync(gradeId);

            if (grade == null)
            {
                throw new Exception("Grade doesnote found");
            }

            var existingGradeSubjects = grade.GradeSubjects?.ToList();

            var existingSubjectIds = existingGradeSubjects?.Select(x => x.Id).ToList()!;

            //get the subjects to add or remove
            // 
            var subjectsToAdd = await _unitOfWork.Subject.FindAsync(s => subjectIds.Contains(s.Id) && !existingSubjectIds.Contains(s.Id));
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
