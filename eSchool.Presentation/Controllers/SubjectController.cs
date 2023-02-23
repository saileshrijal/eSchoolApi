using eSchool.Application.Services.Interfaces;
using eSchool.Domain.Models;
using eSchool.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eSchool.Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var listOfModel = await _subjectService.GetAll();
                var listOfVM = listOfModel.Select(x => new SubjectVM()
                {
                    Id = x.Id,
                    Name = x.Name,
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
                var model = await _subjectService.Get(id);
                var subjectVM = new SubjectVM()
                {
                    Id = id,
                    Name = model.Name,
                };
                return Ok(subjectVM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubjectVM vm)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                var subjectModel = new Subject()
                {
                    Id = vm.Id,
                    Name = vm.Name,
                };
                await _subjectService.Create(subjectModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("id")]
        public async Task<IActionResult> Edit(int id, [FromBody] SubjectVM vm)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                if (vm.Id != id) { return BadRequest("Id does not match"); }
                var subjectModel = new Subject()
                {
                    Id = id,
                    Name = vm.Name,
                };
                await _subjectService.Edit(subjectModel);
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
                await _subjectService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
