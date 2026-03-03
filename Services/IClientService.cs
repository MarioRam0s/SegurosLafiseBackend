using SegurosLafiseBackend.Dtos;

namespace SegurosLafiseBackend.Services
{
    public interface IClientService
    {
        Task<List<ClientDto>> GetAllAsync();
        Task<ClientDto?> GetByIdAsync(int id);
        Task<ClientDto> CreateAsync(CreateClientDto dto);
        Task<bool> DeleteAsync(int id);
        Task<ClientDto> UpdateClientAsync(ClientDto dto);
    }
}
