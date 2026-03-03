using SegurosLafiseBackend.Dtos;
using SegurosLafiseBackend.Entities;
using SegurosLafiseBackend.Repositories;

namespace SegurosLafiseBackend.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _repository;
        public VehicleService(IVehicleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<VehicleDto>> GetAllAsync()
        {
            var vehicles = await _repository.GetAllAsync();

            return vehicles
                .Where(v => v.Active)
                .Select(v => new VehicleDto
                {
                    Id = v.Id,
                    LicensePlate = v.LicensePlate,
                    Brand = v.Brand,
                    Model = v.Model,
                    ManufacturingYear = v.ManufacturingYear.Year,
                    CommercialValue = v.CommercialValue
                })
                .ToList();
        }

        public async Task<VehicleDto?> GetByIdAsync(int id)
        {
            var vehicle = await _repository.GetByIdAsync(id);

            if (vehicle == null || !vehicle.Active)
                return null;

            return new VehicleDto
            {
                Id = vehicle.Id,
                LicensePlate = vehicle.LicensePlate,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                ManufacturingYear = vehicle.ManufacturingYear.Year,
                CommercialValue = vehicle.CommercialValue
            };
        }

        public async Task<VehicleDto> CreateAsync(CreateVehicleDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.LicensePlate))
                throw new Exception("License plate is required");

            if (dto.CommercialValue <= 0)
                throw new Exception("Commercial value must be greater than 0");

            if (dto.ManufacturingYear > DateTime.UtcNow.Year)
                throw new Exception("Manufacturing year cannot be in the future");

            var existing = await _repository.GetByLicensePlateAsync(dto.LicensePlate);

            if (existing != null)
                throw new Exception("Vehicle with this license plate already exists");

            var vehicle = new Vehicle
            {
                LicensePlate = dto.LicensePlate,
                Brand = dto.Brand,
                Model = dto.Model,
                ManufacturingYear = new DateOnly(dto.ManufacturingYear, 1, 1),
                CommercialValue = dto.CommercialValue,
                Active = true,
                CreatedAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            await _repository.AddAsync(vehicle);

            return new VehicleDto
            {
                Id = vehicle.Id,
                LicensePlate = vehicle.LicensePlate,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                ManufacturingYear = vehicle.ManufacturingYear.Year,
                CommercialValue = vehicle.CommercialValue
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vehicle = await _repository.GetByIdAsync(id);

            if (vehicle == null)
                return false;

            vehicle.Active = false;
            vehicle.DeleteAt = DateTime.UtcNow;

            await _repository.UpdateAsync(vehicle);

            return true;
        }
    }
}
