using SegurosLafiseBackend.Dtos;

namespace SegurosLafiseBackend.Services
{
    public interface IInsurancePolicyService
    {
        Task<List<InsurancePolicyDto>> GetAllAsync();
        Task<InsurancePolicyDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);

        Task<InsurancePolicyDto> EmitPolicy(EmitPolicyDto dto);
    }
}
