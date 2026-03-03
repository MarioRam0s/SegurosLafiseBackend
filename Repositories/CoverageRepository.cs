using Microsoft.EntityFrameworkCore;
using SegurosLafiseBackend.Entities;

namespace SegurosLafiseBackend.Repositories
{
    public class CoverageRepository : ICoverageRepository
    {
        private readonly SegurosDbContext _context;

        public CoverageRepository(SegurosDbContext context)
        {
            _context = context;
        }

        public async Task<List<Coverage>> GetAllAsync()
        {
            return await _context.Coverages
                .Where(c => c.Active)
                .ToListAsync();
        }

        public async Task<Coverage?> GetByIdAsync(int id)
        {
            return await _context.Coverages
                .FirstOrDefaultAsync(c => c.Id == id && c.Active);
        }

        public async Task<Coverage> CreateAsync(Coverage coverage)
        {
            await _context.Coverages.AddAsync(coverage);
            await _context.SaveChangesAsync();
            return coverage;
        }

        public async Task SoftDeleteAsync(Coverage coverage)
        {
            coverage.Active = false;
            coverage.DeleteAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}
