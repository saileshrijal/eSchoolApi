using eSchool.Application.Dtos;
using eSchool.Application.Repositories.Interfaces;
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
        private readonly ISubjectRepository _subjectRepository;

        public SubjectController(ISubjectService subjectService,
                                ISubjectRepository subjectRepository)
        {
            _subjectService = subjectService;
            _subjectRepository = subjectRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var subjects = await _subjectRepository.GetAll();
                var result = subjects.Select(x => new
                {
                    x.Id,
                    x.Name,
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
                var subject = await _subjectRepository.GetById(id);
                var result = new
                {
                    subject.Id,
                    subject.Name,
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
