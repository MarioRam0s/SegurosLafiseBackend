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

        // Emitir póliza
        [HttpPost("emitir")]
        public async Task<IActionResult> EmitPolicy([FromBody] EmitPolicyDto dto)
        {
            var result = await _service.EmitPolicy(dto);
            return Ok(result);
        }

        // Obtener todas las pólizas activas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        // Eliminar (soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
