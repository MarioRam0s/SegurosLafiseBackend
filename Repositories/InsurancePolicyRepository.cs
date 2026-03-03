using Microsoft.EntityFrameworkCore;
using SegurosLafiseBackend.Entities;

namespace SegurosLafiseBackend.Repositories
{
    public class InsurancePolicyRepository : IInsurancePolicyRepository
    {
        private readonly SegurosDbContext _context;

        public InsurancePolicyRepository(SegurosDbContext context)
        {
            _context = context;
        }

        public async Task<InsurancePolicy> CreateAsync(InsurancePolicy policy)
        {
            await _context.InsurancePolicies.AddAsync(policy);
            await _context.SaveChangesAsync();
            return policy;
        }

        public async Task<List<InsurancePolicy>> GetAllAsync()
        {
            return await _context.InsurancePolicies
                .Where(p => p.Active)
                .Include(p => p.IdClientNavigation)
                .Include(p => p.IdVehicleNavigation)
                .ToListAsync();
        }

        public async Task<InsurancePolicy?> GetByIdAsync(int id)
        {
            return await _context.InsurancePolicies
                .Include(p => p.InsurancePolicyCoverages)
                .FirstOrDefaultAsync(p => p.Id == id && p.Active);
        }

        public async Task SoftDeleteAsync(InsurancePolicy policy)
        {
            policy.Active = false;
            policy.DeleteAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsActivePolicyByClientAndVehicleAsync(int clientId, int vehicleId)
        {
            return await _context.InsurancePolicies
                .AnyAsync(p =>
                    p.IdClient == clientId &&
                    p.IdVehicle == vehicleId &&
                    p.Active);
        }
    }
}
