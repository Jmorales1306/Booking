using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class ClientRepository(StoreContext context) : IClientRepository
    {
        private readonly StoreContext _context = context;

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client?> GetById(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task Add(Client client)
        {
            await _context.Clients.AddAsync(client);
        }
        public void Update(Client client)
        {
            _context.Clients.Attach(client);
            _context.Entry(client).State = EntityState.Modified;
        }

        public void Delete(Client client)
        {
            _context.Clients.Remove(client);
        }

        public async Task<Client?> GetByEmail(string email)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Email.ToLower() == email.ToLower());
        }

        public async Task<Client?> GetByPhoneNumber(string phoneNumber)
        {
            return await _context.Clients.FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
        }

    }
}