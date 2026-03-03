using Microsoft.EntityFrameworkCore;
using SegurosLafiseBackend.Dtos;
using SegurosLafiseBackend.Entities;
using SegurosLafiseBackend.Repositories;

namespace SegurosLafiseBackend.Services
{
    public class CoverageService : ICoverageService

    {
        private readonly ICoverageRepository _repository;

        public CoverageService(ICoverageRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CoverageDto>> GetAllAsync()
        {
            var coverages = await _repository.GetAllAsync();

            return coverages.Select(c => new CoverageDto
            {
                Id = c.Id,
                IdCoverageCategory = c.IdCoverageCategory,
                Rate = c.Rate
            }).ToList();
        }

        public async Task<CoverageDto> CreateAsync(CreateCoverageDto dto)
        {
            var coverage = new Coverage
            {
                IdCoverageCategory = dto.IdCoverageCategory,
                Rate = dto.Rate,
                Active = true,
            };

            var created = await _repository.CreateAsync(coverage);

            return new CoverageDto
            {
                Id = created.Id,
                IdCoverageCategory = created.IdCoverageCategory,
                Rate = created.Rate,
            };
        }

        public async Task DeleteAsync(int id)
        {
            var coverage = await _repository.GetByIdAsync(id);

            if (coverage is null)
                throw new Exception("Coverage not found");

            await _repository.SoftDeleteAsync(coverage);
        }

        public async Task<CoverageDto?> GetByIdAsync(int id)
        {
            var coverage = await _repository.GetByIdAsync(id);

            if (coverage == null || !coverage.Active)
                return null;

            return new CoverageDto
            {
                Id = coverage.Id,
                IdCoverageCategory = coverage.IdCoverageCategory,
                Rate = coverage.Rate
            };
        }
    }
}
