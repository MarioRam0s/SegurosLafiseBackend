using Microsoft.EntityFrameworkCore;
using SegurosLafiseBackend.Dtos;
using SegurosLafiseBackend.Entities;
using SegurosLafiseBackend.Repositories;

namespace SegurosLafiseBackend.Services
{
    public class InsurancePolicyService : IInsurancePolicyService
    {
        private readonly IInsurancePolicyRepository _policyRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICoverageRepository _coverageRepository;
        private readonly SegurosDbContext _context;

        public InsurancePolicyService(
            IInsurancePolicyRepository policyRepository,
            IClientRepository clientRepository,
            IVehicleRepository vehicleRepository,
            ICoverageRepository coverageRepository,
            SegurosDbContext context)
        {
            _policyRepository = policyRepository;
            _clientRepository = clientRepository;
            _vehicleRepository = vehicleRepository;
            _coverageRepository = coverageRepository;
            _context = context;
        }

        public async Task<List<InsurancePolicyDto>> GetAllAsync()
        {
            var policies = await _policyRepository.GetAllAsync();

            return policies.Select(p => new InsurancePolicyDto
            {
                Id = p.Id,
                PolicyNumber = p.InsurancePolicy1,
                CoverageAmount = p.CoverageAmount,
                TotalPremium = p.TotalPremium
            }).ToList();
        }

        public async Task<InsurancePolicyDto> GetByIdAsync(int id)
        {
            var policy = await _policyRepository.GetByIdAsync(id);

            if (policy is null)
                throw new Exception("Policy not found");

            var vehicle = await _vehicleRepository.GetByIdAsync(policy.IdVehicle);
            if (vehicle is null)
                throw new Exception("Vehicle not found");

            var vehicleAge = DateTime.UtcNow.Year - vehicle.ManufacturingYear;

            if (vehicleAge > 20)
                throw new Exception("The vehicle is older than 20 years and cannot be insured.");

            return new InsurancePolicyDto
            {
                Id = policy.Id,
                PolicyNumber = policy.InsurancePolicy1,
                IdClient = policy.IdClient,
                IdVehicle = policy.IdVehicle,
                IssueDate = policy.IssueDate,
                CoverageAmount = policy.CoverageAmount,
                TotalPremium = policy.TotalPremium,
                Active = policy.Active
            };
        }

        public async Task DeleteAsync(int id)
        {
            var policy = await _policyRepository.GetByIdAsync(id);

            if (policy is null)
                throw new Exception("Policy not found");

            await _policyRepository.SoftDeleteAsync(policy);
        }

        public async Task<InsurancePolicyDto> EmitPolicy(EmitPolicyDto dto)
        {
            // 🔹 1. Validar cliente
            var client = await _clientRepository.GetByIdAsync(dto.IdClient);
            if (client == null)
                throw new Exception("Client not found");

            // 🔹 2. Validar vehículo
            var vehicle = await _vehicleRepository.GetByIdAsync(dto.IdVehicle);
            if (vehicle == null)
                throw new Exception("Vehicle not found");

            // 🔹 3. Validar que no exista póliza activa para este cliente y vehículo
            var hasActivePolicy = await _policyRepository.ExistsActivePolicyByClientAndVehicleAsync(client.Id, vehicle.Id);
            if (hasActivePolicy)
                throw new Exception("Client already has an active policy for this vehicle");

            // 🔹 4. Obtener coberturas seleccionadas
            var coverages = await _coverageRepository.GetByIdsAsync(dto.CoverageIds);
            if (coverages.Count != dto.CoverageIds.Count)
                throw new Exception("One or more coverages not found");

            // 🔹 5. Calcular TotalPremium
            decimal totalPremium = coverages.Sum(c => vehicle.CommercialValue * (c.Rate / 100m));

            // 🔹 6. Crear póliza
            var policy = new InsurancePolicy
            {
                InsurancePolicy1 = "POL-" + DateTime.UtcNow.Ticks, // número único
                IdClient = client.Id,
                IdVehicle = vehicle.Id,
                IssueDate = DateOnly.FromDateTime(DateTime.UtcNow),
                CoverageAmount = vehicle.CommercialValue,
                TotalPremium = totalPremium,
                Active = true,
                CreatedAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            // 🔹 7. Asociar coberturas a la póliza
            foreach (var coverage in coverages)
            {
                policy.InsurancePolicyCoverages.Add(new InsurancePolicyCoverage
                {
                    IdCoverage = coverage.Id,
                    IdPolicyNavigation = policy,
                    AppliedRate = coverage.Rate,
                    AppliedCoverageAmount = vehicle.CommercialValue * (coverage.Rate / 100m),
                    Active = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                });
            }

            // 🔹 8. Guardar póliza
            var createdPolicy = await _policyRepository.CreateAsync(policy);

            // 🔹 9. Devolver DTO
            return new InsurancePolicyDto
            {
                Id = createdPolicy.Id,
                PolicyNumber = createdPolicy.InsurancePolicy1,
                IdClient = client.Id,
                IdVehicle = vehicle.Id,
                IssueDate = createdPolicy.IssueDate,
                CoverageAmount = createdPolicy.CoverageAmount,
                TotalPremium = createdPolicy.TotalPremium,
                Active = createdPolicy.Active
            };
        }

    }
}
