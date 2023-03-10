using eSchool.Application.Dtos;
using eSchool.Application.Repositories.Interfaces;
using eSchool.Presentation.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.Application.Services.Interface;

namespace eSchool.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;
        private readonly IGradeRepository _gradeRepository;
        private readonly IGradeSubjectRepository _gradeSubjectRepository;

        public GradeController(IGradeService gradeService,
                                IGradeRepository gradeRepository,
                                IGradeSubjectRepository gradeSubjectRepository)
        {
            _gradeService = gradeService;
            _gradeRepository = gradeRepository;
            _gradeSubjectRepository = gradeSubjectRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var grades = await _gradeRepository.GetAll();
                
                var result = grades.Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Section
                }).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var grade = await _gradeRepository.GetById(id);

                var result = new
                {
                    grade.Id,
                    grade.Name,
                    grade.Section
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] GradeVM vm)
        {
            try
            {
                var gradeDto = new GradeDto()
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Section = vm.Section
                };
                await _gradeService.CreateGradeAsync(gradeDto);
                return Ok("Grade Created Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] GradeVM vm)
        {
            try
            {
                var gradeDto = new GradeDto()
                {
                    Name = vm.Name,
                    Section = vm.Section
                };
                await _gradeService.UpdateGradeAsync(id, gradeDto);
                return Ok("Grade updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _gradeService.DeleteGradeAsync(id);
                return Ok("Grade deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> AddSubjectsToGrade(int id, List<int> SubjectIds)
        {
            try
            {
                await _gradeService.AddSubjectsToGrade(id, SubjectIds);
                return Ok("Subjects added to grade successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateGradeSubjects(int id, List<int> SubjectIds)
        {
            try
            {
                await _gradeService.UpdateGradeSubjects(id, SubjectIds);
                return Ok("Subjects added to grade successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetGradesWithSubjects()
        {
            try
            {
                var gradesSubjctsDto = await _gradeSubjectRepository.GetGradesWithSubjects();
                return Ok(gradesSubjctsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
