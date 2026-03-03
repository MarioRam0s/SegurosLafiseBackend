using SegurosLafiseBackend.Dtos;
using SegurosLafiseBackend.Entities;
using SegurosLafiseBackend.Repositories;

namespace SegurosLafiseBackend.Services
{
    public class CoverageCategoryService : ICoverageCategoryService
    {
        private readonly ICoverageCategoryRepository _repository;

        public CoverageCategoryService(ICoverageCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CoverageCategoryDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();

            return categories
                .Where(c => c.Active)
                .Select(c => new CoverageCategoryDto
                {
                    Id = c.Id,
                    NameCategory = c.NameCategory
                })
                .ToList();
        }

        public async Task<CoverageCategoryDto> CreateAsync(CreateCoverageCategoryDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.NameCategory))
                throw new Exception("Category name is required");

            var existing = await _repository.GetByNameAsync(dto.NameCategory);

            if (existing != null)
                throw new Exception("Category already exists");

            var category = new CoverageCategory
            {
                NameCategory = dto.NameCategory,
                Active = true,
                CreatedAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            await _repository.AddAsync(category);

            return new CoverageCategoryDto
            {
                Id = category.Id,
                NameCategory = category.NameCategory
            };
        }
    }
}
