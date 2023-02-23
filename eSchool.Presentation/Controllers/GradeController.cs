
using eSchool.Application.Services.Interfaces;
using eSchool.Domain.Models;
using eSchool.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eSchool.Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var listOfModel = await _gradeService.GetAll();
                var listOfVM = listOfModel.Select(x => new GradeVM()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Section = x.Section,
                });
                return Ok(listOfVM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await _gradeService.Get(id);
                var gradeVM = new GradeVM()
                {
                    Id = id,
                    Name = model.Name,
                    Section = model.Section,
                };
                return Ok(gradeVM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GradeVM vm)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                var gradeModel = new Grade()
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Section = vm.Section,
                };
                await _gradeService.Create(gradeModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("id")]
        public async Task<IActionResult> Edit(int id, [FromBody] GradeVM vm)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                if (vm.Id != id) { return BadRequest("Id does not match"); }
                var gradeModel = new Grade()
                {
                    Id = id,
                    Name = vm.Name,
                    Section = vm.Section
                };
                await _gradeService.Edit(gradeModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _gradeService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
