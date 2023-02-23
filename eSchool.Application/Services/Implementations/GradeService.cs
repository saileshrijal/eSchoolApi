using eSchool.Application.Services.Interfaces;
using eSchool.Domain.Models;
using eSchool.Infrastructure.UnitOfWork.Interface;

namespace eSchool.Application.Services.Implementations
{
    public class GradeService:IGradeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GradeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Create(Grade grade)
        {
            var checkGradeByNameSection = await _unitOfWork.Grade.GetBy(x => x.Name!.ToLower().Trim() == grade.Name!.ToLower().Trim()
                                                    && x.Section!.ToLower().Trim() == grade.Section!.ToLower().Trim());

            if (checkGradeByNameSection != null)
            {
                throw new DuplicateWaitObjectException($"Grade: {grade.Name} already exists");
            }

            await _unitOfWork.Grade.Create(grade);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> Delete(int id)
        {
            var checkGrade = await _unitOfWork.Grade.GetBy(x => x.Id == id);
            if (checkGrade == null)
            {
                throw new Exception($"Grade with ID: {id} does not exists");
            }
            await _unitOfWork.Grade.Delete(id);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> Edit(Grade grade)
        {
            var checkGradeByNameSection = await _unitOfWork.Grade.GetBy(x => x.Name!.ToLower().Trim() == grade.Name!.ToLower().Trim()
                                                    && x.Section!.ToLower().Trim() == grade.Section!.ToLower().Trim());

            if (checkGradeByNameSection != null)
            {
                if (checkGradeByNameSection.Id != grade.Id)
                {
                    throw new DuplicateWaitObjectException($"Grade: {grade.Name} already exists");
                }
            }
            _unitOfWork.Grade.Edit(grade);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<Grade> Get(int id)
        {
            var grade = await _unitOfWork.Grade.GetBy(x => x.Id == id);
            if (grade == null)
            {
                throw new Exception($"Grade with ID: {id} does not exists");
            }
            return grade;
        }

        public async Task<List<Grade>> GetAll()
        {
            return await _unitOfWork.Grade.GetAll();
        }
    }
}
