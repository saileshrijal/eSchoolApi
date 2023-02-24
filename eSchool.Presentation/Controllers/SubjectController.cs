using eSchool.Application.Dtos;
using eSchool.Application.Services.Interfaces;
using eSchool.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eSchool.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var subjectsDto = await _subjectService.GetAllSubjectAsync();
                var result = subjectsDto.Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.GradeId
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
                var subjectDto = await _subjectService.GetSubjectByIdAsync(id);
                var result = new
                {
                    subjectDto.Id,
                    subjectDto.Name,
                    subjectDto.GradeId
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SubjectVM vm)
        {
            try
            {
                var subjectDto = new SubjectDto()
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    GradeId = vm.GradeId 
                };
                await _subjectService.CreateSubjectAsync(subjectDto);
                return Ok("Subject Created Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] SubjectVM vm)
        {
            try
            {
                var subjectDto = new SubjectDto()
                {
                    Name = vm.Name,
                };
                await _subjectService.UpdateSubjectAsync(id, subjectDto);
                return Ok("Subject updated successfully");
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
                await _subjectService.DeleteSubjectAsync(id);
                return Ok("Subject deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
