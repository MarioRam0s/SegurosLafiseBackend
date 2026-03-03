using Microsoft.EntityFrameworkCore;
using SegurosLafiseBackend.Entities;

namespace SegurosLafiseBackend.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly SegurosDbContext _context;

        public ClientRepository(SegurosDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Client?> GetByIdentificationAsync(string identification)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(c => c.Identification == identification);
        }

        public async Task AddAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }
    }
}
