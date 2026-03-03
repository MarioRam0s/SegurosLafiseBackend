using SegurosLafiseBackend.Entities;

namespace SegurosLafiseBackend.Repositories
{
    public interface IInsurancePolicyRepository
    {
        Task<InsurancePolicy> CreateAsync(InsurancePolicy policy);
        Task<List<InsurancePolicy>> GetAllAsync();
        Task<InsurancePolicy?> GetByIdAsync(int id);
        Task SoftDeleteAsync(InsurancePolicy policy);
    }
}
