using SegurosLafiseBackend.Entities;

namespace SegurosLafiseBackend.Repositories
{
    public interface ICoverageRepository
    {
        Task<List<Coverage>> GetAllAsync();
        Task<Coverage?> GetByIdAsync(int id);
        Task<Coverage> CreateAsync(Coverage coverage);
        Task SoftDeleteAsync(Coverage coverage);

        Task<List<Coverage>> GetByIdsAsync(List<int> ids);
    }
}
