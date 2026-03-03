using Microsoft.AspNetCore.Mvc;
using SegurosLafiseBackend.Dtos;
using SegurosLafiseBackend.Services;

namespace SegurosLafiseBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsurancePolicyController : ControllerBase
    {

        private readonly IInsurancePolicyService _service;

        public InsurancePolicyController(IInsurancePolicyService service)
        {
            _service = service;
        }

        // 🔹 Crear póliza
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInsurancePolicyDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        // 🔹 Obtener todas las pólizas activas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // 🔹 Eliminar (soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
