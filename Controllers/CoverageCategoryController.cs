using Microsoft.AspNetCore.Mvc;
using SegurosLafiseBackend.Dtos;
using SegurosLafiseBackend.Services;

namespace SegurosLafiseBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoverageCategoryController : ControllerBase
    {
        private readonly ICoverageCategoryService _service;

        public CoverageCategoryController(ICoverageCategoryService service)
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
        public async Task<IActionResult> Create(CreateCoverageCategoryDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }
    }
}
