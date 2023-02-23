using eSchool.Application.Dtos;
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

        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var gradesDto = await _gradeService.GetAllGradesAsync();
                var result = gradesDto.Select(x => new
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
                var gradesDto = await _gradeService.GetGradeByIdAsync(id);
                var result = new
                {
                    gradesDto.Id,
                    gradesDto.Name,
                    gradesDto.Section
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
    }
}
