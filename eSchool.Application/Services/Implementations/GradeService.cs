using eSchool.Application.Dtos;
using eSchool.Domain.Models;
using eSchool.Infrastructure.UnitOfWork.Interface;
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

        public async Task CreateGradeAsync(GradeDto gradeDto)
        {
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

        public async Task UpdateGradeAsync(int id, GradeDto gradeDto)
        {
            var grade = await _unitOfWork.Grade.GetByIdAsync(id);
            if (grade == null) { throw new Exception("Grade does not found"); }
            grade.Name = gradeDto.Name;
            grade.Section = gradeDto.Section;
            await _unitOfWork.SaveAsync();
        }
    }
}
