using SegurosLafiseBackend.Entities;

namespace SegurosLafiseBackend.Repositories
{
    public interface ICoverageCategoryRepository
    {
        Task<List<CoverageCategory>> GetAllAsync();
        Task<CoverageCategory?> GetByIdAsync(int id);
        Task<CoverageCategory?> GetByNameAsync(string name);
        Task AddAsync(CoverageCategory category);
        Task UpdateAsync(CoverageCategory category);
    }
}
