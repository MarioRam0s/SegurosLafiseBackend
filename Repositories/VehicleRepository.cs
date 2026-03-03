using Microsoft.EntityFrameworkCore;
using SegurosLafiseBackend.Entities;

namespace SegurosLafiseBackend.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly SegurosDbContext _context;

        public VehicleRepository(SegurosDbContext context)
        {
            _context = context;
        }

        public async Task<List<Vehicle>> GetAllAsync()
        {
            return await _context.Vehicles.ToListAsync();
        }

        public async Task<Vehicle?> GetByIdAsync(int id)
        {
            return await _context.Vehicles
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Vehicle?> GetByLicensePlateAsync(string licensePlate)
        {
            return await _context.Vehicles
                .FirstOrDefaultAsync(v => v.LicensePlate == licensePlate);
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
        }
    }
}
