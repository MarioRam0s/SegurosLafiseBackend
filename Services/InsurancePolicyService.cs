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

        public async Task<InsurancePolicyDto> CreateAsync(CreateInsurancePolicyDto dto)
        {
            // 🔹 Validar cliente
            var client = await _clientRepository.GetByIdAsync(dto.IdClient);
            if (client is null)
                throw new Exception("Client not found");

            // 🔹 Validar vehículo
            var vehicle = await _vehicleRepository.GetByIdAsync(dto.IdVehicle);
            if (vehicle is null)
                throw new Exception("Vehicle not found");

            // 🔹 Obtener coberturas seleccionadas
            var coverages = await _coverageRepository.GetAllAsync();
            var selectedCoverages = coverages
                .Where(c => dto.CoverageIds.Contains(c.Id))
                .ToList();

            if (!selectedCoverages.Any())
                throw new Exception("At least one coverage must be selected");

            decimal totalPremium = 0;

            var policy = new InsurancePolicy
            {
                InsurancePolicy1 = dto.PolicyNumber,
                IdClient = dto.IdClient,
                IdVehicle = dto.IdVehicle,
                IssueDate = dto.IssueDate,
                CoverageAmount = dto.CoverageAmount,
                Active = true,
                CreatedAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            await _policyRepository.CreateAsync(policy);

            foreach (var coverage in selectedCoverages)
            {
                var appliedRate = coverage.Rate;
                var appliedAmount = vehicle.CommercialValue * appliedRate;

                totalPremium += appliedAmount;

                var policyCoverage = new InsurancePolicyCoverage
                {
                    IdPolicy = policy.Id,
                    IdCoverage = coverage.Id,
                    AppliedRate = appliedRate,
                    AppliedCoverageAmount = appliedAmount,
                    Active = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };

                _context.InsurancePolicyCoverages.Add(policyCoverage);
            }

            policy.TotalPremium = totalPremium;
            await _context.SaveChangesAsync();

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

        public async Task DeleteAsync(int id)
        {
            var policy = await _policyRepository.GetByIdAsync(id);

            if (policy is null)
                throw new Exception("Policy not found");

            await _policyRepository.SoftDeleteAsync(policy);
        }
    }
}
