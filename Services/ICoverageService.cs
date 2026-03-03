using SegurosLafiseBackend.Dtos;
using SegurosLafiseBackend.Entities;

namespace SegurosLafiseBackend.Services
{
    public interface ICoverageService

    {
        Task<List<CoverageDto>> GetAllAsync();
        Task<CoverageDto> CreateAsync(CreateCoverageDto dto);
        Task DeleteAsync(int id);
    }
}
