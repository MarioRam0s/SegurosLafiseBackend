using SegurosLafiseBackend.Dtos;
using SegurosLafiseBackend.Entities;
using SegurosLafiseBackend.Repositories;

namespace SegurosLafiseBackend.Services
{
    public class ClientService : IClientService
    {

        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<ClientDto> CreateAsync(CreateClientDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.NameClient))
                throw new Exception("Client name is required");

            var existingClient = await _repository.GetByIdentificationAsync(dto.Identification);

            if (existingClient != null)
                throw new Exception("Client with this identification already exists");

            var client = new Client
            {
                NameClient = dto.NameClient,
                Identification = dto.Identification,
                Email = dto.Email,
                Active=true,
            };

            await _repository.AddAsync(client);

            return new ClientDto
            {
                Id = client.Id,
                NameClient = client.NameClient,
                Identification = client.Identification,
                Email = client.Email
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var client = await _repository.GetByIdAsync(id);

            if (client == null)
                return false;

            client.Active = false;
            client.DeleteAt = DateTime.UtcNow;

            await _repository.UpdateAsync(client);

            return true;
        }

        public async Task<List<ClientDto>> GetAllAsync()
        {
            var clients = await _repository.GetAllAsync();

            return clients
                .Where(c => c.Active)
                .Select(c => new ClientDto
                {
                    Id = c.Id,
                    NameClient = c.NameClient,
                    Identification = c.Identification,
                    Email = c.Email
                })
                .ToList();
        }

        public async Task<ClientDto?> GetByIdAsync(int id)
        {
            var client = await _repository.GetByIdAsync(id);

            if (client == null || !client.Active)
                return null;

            return new ClientDto
            {
                Id = client.Id,
                NameClient = client.NameClient,
                Identification = client.Identification,
                Email = client.Email
            };
        }
    }
}
