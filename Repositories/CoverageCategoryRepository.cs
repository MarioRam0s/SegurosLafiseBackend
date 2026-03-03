using Microsoft.EntityFrameworkCore;
using SegurosLafiseBackend.Entities;

namespace SegurosLafiseBackend.Repositories
{
    public class CoverageCategoryRepository : ICoverageCategoryRepository
    {
        private readonly SegurosDbContext _context;

        public CoverageCategoryRepository(SegurosDbContext context)
        {
            _context = context;
        }

        public async Task<List<CoverageCategory>> GetAllAsync()
        {
            return await _context.CoverageCategories.ToListAsync();
        }

        public async Task<CoverageCategory?> GetByIdAsync(int id)
        {
            return await _context.CoverageCategories
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CoverageCategory?> GetByNameAsync(string name)
        {
            return await _context.CoverageCategories
                .FirstOrDefaultAsync(c => c.NameCategory == name);
        }

        public async Task AddAsync(CoverageCategory category)
        {
            await _context.CoverageCategories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CoverageCategory category)
        {
            _context.CoverageCategories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
