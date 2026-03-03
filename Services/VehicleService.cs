using SegurosLafiseBackend.Dtos;
using SegurosLafiseBackend.Entities;
using SegurosLafiseBackend.Repositories;
using System.Text.RegularExpressions;

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
                    ManufacturingYear = v.ManufacturingYear,
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
                ManufacturingYear = vehicle.ManufacturingYear,
                CommercialValue = vehicle.CommercialValue
            };
        }

        public async Task<VehicleDto> CreateAsync(CreateVehicleDto dto)
        {
            // 🔹 Validaciones básicas
            if (string.IsNullOrWhiteSpace(dto.LicensePlate))
                throw new Exception("License plate is required");

            // 🔹 Validar formato de placa (ej: ABC-1234)
            var platePattern = @"^[A-Z]{1,2}\s?\d{3,4}(-[A-Z]{0,3})?$";
            if (!Regex.IsMatch(dto.LicensePlate.ToUpper(), platePattern))
                throw new Exception("License plate format is invalid. Expected format: C 1234-ABC, CD 1234, M 123-456");

            if (dto.CommercialValue <= 0)
                throw new Exception("Commercial value must be greater than 0");

            if (dto.ManufacturingYear > DateTime.UtcNow.Year)
                throw new Exception("Manufacturing year cannot be in the future");

            // 🔹 Validar que la placa no exista
            var existing = await _repository.GetByLicensePlateAsync(dto.LicensePlate);
            if (existing != null)
                throw new Exception("Vehicle with this license plate already exists");

            // 🔹 Crear vehículo
            var vehicle = new Vehicle
            {
                LicensePlate = dto.LicensePlate.ToUpper(),
                Brand = dto.Brand,
                Model = dto.Model,
                ManufacturingYear = dto.ManufacturingYear,
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
                ManufacturingYear = vehicle.ManufacturingYear,
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
