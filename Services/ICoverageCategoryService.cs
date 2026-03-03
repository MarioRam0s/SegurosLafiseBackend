using SegurosLafiseBackend.Dtos;

namespace SegurosLafiseBackend.Services
{
    public interface ICoverageCategoryService
    {
        Task<List<CoverageCategoryDto>> GetAllAsync();

        Task<CoverageCategoryDto> CreateAsync(CreateCoverageCategoryDto dto);

    }
}
