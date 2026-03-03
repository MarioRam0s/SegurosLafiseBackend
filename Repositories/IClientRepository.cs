using SegurosLafiseBackend.Entities;

namespace SegurosLafiseBackend.Repositories
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(int id);
        Task<Client?> GetByIdentificationAsync(string identification);
        Task AddAsync(Client client);
        Task UpdateAsync(Client client);
    }
}
