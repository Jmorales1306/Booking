using Domain.Interface;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class RoleRepository(StoreContext storeContext) : IRoleRepository
    {

        private readonly StoreContext _context = storeContext;

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> GetById(int id)
        {
            return await _context.Roles.FindAsync(id);
        }
        public async Task Add(Role role)
        {
            await _context.Roles.AddAsync(role);
        }
        public void Update(Role role)
        {
            _context.Roles.Attach(role);
            _context.Entry(role).State = EntityState.Modified;
        }

        public void Delete(Role role)
        {
            _context.Remove(role);
        }

        public async Task<Role?> GetByName(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
        }
    }
}