using Microsoft.AspNetCore.Mvc;
using SegurosLafiseBackend.Dtos;
using SegurosLafiseBackend.Services;

namespace SegurosLafiseBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoverageController : ControllerBase
    {
        private readonly ICoverageService _service;

        public CoverageController(ICoverageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCoverageDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
