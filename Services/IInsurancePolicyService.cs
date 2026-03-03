using SegurosLafiseBackend.Dtos;

namespace SegurosLafiseBackend.Services
{
    public interface IInsurancePolicyService
    {
        Task<InsurancePolicyDto> CreateAsync(CreateInsurancePolicyDto dto);
        Task<List<InsurancePolicyDto>> GetAllAsync();
        Task DeleteAsync(int id);
    }
}
