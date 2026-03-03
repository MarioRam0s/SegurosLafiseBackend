using SegurosLafiseBackend.Dtos;

namespace SegurosLafiseBackend.Services
{
    public interface IVehicleService
    {
        Task<List<VehicleDto>> GetAllAsync();
        Task<VehicleDto?> GetByIdAsync(int id);
        Task<VehicleDto> CreateAsync(CreateVehicleDto dto);
        Task<bool> DeleteAsync(int id);

        Task<VehicleDto> UpdateVehicleAsync(int id, VehicleDto dto);
    }
}
