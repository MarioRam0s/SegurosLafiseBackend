using SegurosLafiseBackend.Entities;

namespace SegurosLafiseBackend.Repositories
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetAllAsync();
        Task<Vehicle?> GetByIdAsync(int id);
        Task<Vehicle?> GetByLicensePlateAsync(string licensePlate);
        Task AddAsync(Vehicle vehicle);
        Task UpdateAsync(Vehicle vehicle);
    }
}
